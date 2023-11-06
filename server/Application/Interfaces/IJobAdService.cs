using JobPortal.Application.ViewModels.ResponseM;

namespace JobPortal.Application.Interfaces
{
    public interface IJobAdService
    {
        public ResponseViewModel addJobAd(CreateJobAdModel model,string authToken);
        public ResponseViewModel deleteJobAd(Guid id);
    }
}
