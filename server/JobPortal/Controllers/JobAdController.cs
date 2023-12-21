using JobPortal.Application.Interfaces;
using JobPortal.Application.ViewModels.AuthM;
using JobPortal.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        public ResponseViewModel add(Guid id)
        {
            return jobAdService.getJobAd(id);
        }

        [HttpPost("add"), Authorize(Roles = "Employer, Admin")]
        public ResponseViewModel add(CreateJobAdModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return jobAdService.addJobAd(model,authToken);
        }

        [HttpPut("update"), Authorize(Roles = "Employer, Admin")]
        public ResponseViewModel update(UpdateJobAdModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return jobAdService.updateJobAd(model,authToken);
        }

        [HttpDelete("delete"), Authorize(Roles = "Employer, Admin")]
        public ResponseViewModel delete(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return jobAdService.deleteJobAd(id,authToken);
        }

    }
}
