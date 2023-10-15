using JobPortal.Domain.Entities;

namespace JobPortal.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Role getRole(string roleName);
    }
}
