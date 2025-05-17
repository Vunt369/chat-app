using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Services
{
    public interface IMessageService
    {
        Task<Message> GetMessageAsync(int id);
        Task<IEnumerable<Message>> GetAllMessagesAsync();
        Task<bool> CreateMessageAsync(Message message);
        Task<bool> UpdateMessageAsync(Message message);
        Task<bool> DeleteMessageAsync(int id);
    }
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateMessageAsync(Message message)
        {
            if(message == null)
                throw new ArgumentNullException("Message is required");
            await _unitOfWork.MessageRepository.AddAsync(message);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            if (id == null)
                throw new ArgumentNullException("MessageId is required");
            var message = await _unitOfWork.MessageRepository.GetByIdAsync(id);
            if (message == null)
                throw new KeyNotFoundException($"Message with ID {id} not found");

            await _unitOfWork.MessageRepository.RemoveAsync(message);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _unitOfWork.MessageRepository.GetAllAsync();
        }

        public async Task<Message> GetMessageAsync(int id)
        {
            var message = await _unitOfWork.MessageRepository.GetByIdAsync(id);
            if (message == null)
                throw new KeyNotFoundException($"Message with ID {id} not found");

            return message;
        }

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message), "Message is required");

            var existingMessage = await _unitOfWork.MessageRepository.GetByIdAsync(message.Id);
            if (existingMessage == null)
                return false;

            _unitOfWork.MessageRepository.Update(message);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
