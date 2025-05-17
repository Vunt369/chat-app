using ChatApp.Core.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Services
{
    public interface IChatService
    {
        Task<bool> SendMessageAsync(string receiverId, string message);
    }
    public class ChatService : IChatService
    {
        private readonly IChatHub _chatHub;
        public ChatService(IChatHub chatHub)
        {
            _chatHub = chatHub;
        }
        public async Task<bool> SendMessageAsync(string receiverId, string message)
        {
           return await _chatHub.SendMessage(receiverId, message);
        }
    }
}
