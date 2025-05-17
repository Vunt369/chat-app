using ChatApp.Core.Data;
using ChatApp.Core.Interfaces;
using ChatApp.Core.Models;

namespace ChatApp.Core.Implements
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        private IGenericRepository<ApplicationUser> _userRepository;
        private IGenericRepository<Message> _messageRepository;

        public IGenericRepository<ApplicationUser> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<ApplicationUser>(_dbContext);
                }
                return _userRepository;
            }
        }

        public IGenericRepository<Message> MessageRepository
        {
            get
            {
                if (_messageRepository == null)
                {
                    _messageRepository = new GenericRepository<Message>(_dbContext);
                }
                return _messageRepository;
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}