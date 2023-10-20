using JobPortal.Domain.Entities;

namespace JobPortal.Application.Interfaces
{
    public interface IUserService
    {
        public User update(User user);
        public User getUserByRefreshToken(string token);
        public User getUserByUsername(string username);
        public User add(User user);
        public User get(Guid id);
        public User getUserByGoogleId(string GoogleId);
    }
}
