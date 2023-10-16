using JobPortal.Domain.Entities;

namespace JobPortal.Application.Interfaces
{
    public interface IRoleService
    {
        public Role get(Guid id);
        public Role getRole(string roleName);
    }
}
