using ChatApp.Core.Models;

namespace ChatApp.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<ApplicationUser> UserRepository { get; }
        IGenericRepository<Message> MessageRepository { get; }
        Task<bool> SaveChangesAsync();
        
    }
}