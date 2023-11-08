using JobPortal.Domain.Entities;
using System.Net;

namespace JobPortal.Domain.Interfaces
{
    public interface IJobAdRepository : IRepository<JobAd>
    {
        public JobAd getJobAdWithPhotos(Guid id);
    }
}
