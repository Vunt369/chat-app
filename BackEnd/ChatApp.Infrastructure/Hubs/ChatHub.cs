using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using ChatApp.Infrastructure.Hubs;
using ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace ChatApp.Core.Hubs
{
    public interface IChatHub
    {
        Task<bool> SendMessage(string receiverId, string message);
    }

    [Authorize]
    public class ChatHub : BaseHub, IChatHub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisService _redisService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public ChatHub(IUnitOfWork unitOfWork, IRedisService redisService, IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _redisService = redisService;
            _messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var connectionId = Context.ConnectionId;

            if (string.IsNullOrEmpty(userId))
            {
                await base.OnConnectedAsync();
                return;
            }
            try
            {
                string cacheKey = $"user_connections:{userId}";
                await _redisService.AddToSetAsync(cacheKey, connectionId);

                var connections = await _redisService.GetSetAsync(cacheKey);
                if (connections.Count() == 1)
                {
                    await _redisService.AddToSetAsync("user_connections", userId);
                    await Clients.All.SendAsync("UserOnline", userId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnConnectedAsync: {ex.Message}");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId))
            {
                string cacheKey = $"user_connections:{userId}";        
                await _redisService.RemoveFromSetAsync(cacheKey, connectionId);

                var remainingConnections = await _redisService.GetSetAsync(cacheKey);
                if (!remainingConnections.Any())
                {
                    await _redisService.RemoveFromSetAsync("user_connections", userId);
                    await Clients.All.SendAsync("UserOffline", userId);
                }
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task<bool> SendMessage(string receiverId, string message)
        {
            var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var newMessage = new Message
            //{
            //    SenderId = senderId,
            //    ReceiverId = receiverId,
            //    Content = message
            //};
            //var result = await _messageService.CreateMessageAsync(newMessage);

            if (1 == 1)
            {
                // Gửi tin nhắn realtime qua SignalR
                await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);
                return true;
            }
            return false;
        }

        //public static List<User> GetOnlineUsers()
        //{
        //    var users = OnlineUsers.Values.ToList();
        //    return users;
        //}
    }
}

internal class User
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string UserId { get; set; }
    public string Avatar { get; set; }
}