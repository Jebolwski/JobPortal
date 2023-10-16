using JobPortal.Domain.Interfaces;
using JobPortal.Data.Context;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BaseContext db) : base(db)
        {
        }

        public User getUserByUsername(string name)
        {
            IQueryable<User> users = dbset.Where(p => p.name == name);
            if (users != null && users.Any())
            {
                User user = users.First();
                return user;
            }
            return null;
        }

        public User getUserByRefreshToken(string token)
        {
            IQueryable<User> users = dbset.Where(p => p.RefreshToken == token);
            if (users != null && users.Any())
            {
                return users.First();
            }
            return null;
        }


    }
}
