using JobPortal.Domain.Entities;

namespace JobPortal.Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        public T add(T entity);
        public T update(T entity);
        public bool delete(Guid id);
        public T get(Guid id);
        public IQueryable<T> getAll();
    }
}
