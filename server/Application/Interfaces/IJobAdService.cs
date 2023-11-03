using JobPortal.Application.ViewModels.AuthM;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Application.Interfaces
{
    public interface IJobAdService
    {
        public ResponseViewModel addJobAd(CreateJobAdModel model);
        public ResponseViewModel deleteJobAd(Guid id);
    }
}
