using JobPortal.Data.Context;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Data.Repositories
{
    public class JobAdRepository : Repository<JobAd>, IJobAdRepository
    {
        public JobAdRepository(BaseContext db) : base(db)
        {
        }
    }
}
