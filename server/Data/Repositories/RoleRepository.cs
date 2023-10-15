using JobPortal.Data.Context;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(BaseContext db) : base(db)
        {
        }

        public Role getRole(string roleName)
        {
            ICollection<Role> roles = dbset.Where(p=>p.name== roleName).ToList();
            if (roles!=null && roles.Any())
            {
                return roles.First();
            }
            return null;
        }
    }
}
