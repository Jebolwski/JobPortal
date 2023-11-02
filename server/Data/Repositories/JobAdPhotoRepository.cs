using JobPortal.Data.Context;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Data.Repositories
{
    public class JobAdPhotoRepository : Repository<JobAdPhoto>, IJobAdPhotoRepository
    {
        public JobAdPhotoRepository(BaseContext db) : base(db)
        {
        }
    }
}
