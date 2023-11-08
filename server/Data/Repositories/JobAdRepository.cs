using System.Security.Cryptography.X509Certificates;
using JobPortal.Data.Context;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Data.Repositories
{
    public class JobAdRepository : Repository<JobAd>, IJobAdRepository
    {
        public JobAdRepository(BaseContext db) : base(db)
        {
        }

        public JobAd getJobAdWithPhotos(Guid id){
            JobAd jobAd = dbset.AsTracking().Where(p => p.id == id).Include(x => x.photos).ToList().First();
            return jobAd;
        }
    }
}
