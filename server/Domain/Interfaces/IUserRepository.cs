using JobPortal.Domain.Entities;

namespace JobPortal.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User getUserByUsername(string name);
        public User getUserByRefreshToken(string token);
    }
}
