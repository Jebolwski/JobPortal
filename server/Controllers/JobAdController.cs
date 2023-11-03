using JobPortal.Application.Interfaces;
using JobPortal.Application.ViewModels.AuthM;
using JobPortal.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAdController : ControllerBase
    {
        private readonly IJobAdService jobAdService;

        public JobAdController(IJobAdService jobAdService)
        {
            this.jobAdService = jobAdService;
        }

        [HttpPost("add")]
        public ResponseViewModel Register(CreateJobAdModel model)
        {
            return jobAdService.addJobAd(model);
        }

        [HttpDelete("delete")]
        public ResponseViewModel Delete(Guid id)
        {
            return jobAdService.deleteJobAd(id);
        }

    }
}
