using ChatApp.Attributes;
using ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        public ChatController(IChatService chatService, IMessageService messageService)
        {
             _chatService = chatService;
            _messageService = messageService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessage(string receiverId,  string message)
        {
            var result = await _chatService.SendMessageAsync(receiverId, message);
            if (result) return Ok("Send Message Successfully");
            return BadRequest("Send Message Error");
        }
        [HttpGet("messages")]
        [Cache(1000)] // Cache API trong n giây
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return Ok(messages);
        }
    }
}
