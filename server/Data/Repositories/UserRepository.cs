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

    }
}
