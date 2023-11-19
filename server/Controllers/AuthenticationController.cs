using JobPortal.Application.Interfaces;
using JobPortal.Application.ViewModels.AuthM;
using JobPortal.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public ResponseViewModel Register(RegisterModel model)
        {
            return authenticationService.Register(model);
        }

        [HttpPost("add-user")]
        public ResponseViewModel AddUser(GoogleCreateUserModel model)
        {
            return authenticationService.addGoogleUser(model);
        }

        [HttpGet("get-user/{id}")]
        public ResponseViewModel getUser(Guid id)
        {
            return authenticationService.getUser(id);
        }

        [HttpPost("login")]
        public ResponseViewModel Login(LoginModel model)
        {
            return authenticationService.Login(model);
        }

        [HttpPost("refresh-token")]
        public ResponseViewModel RefreshToken(RefreshTokenModel model)
        {
            return authenticationService.RefreshToken(model);
        }

        [HttpPost("search-by-username")]
        public ResponseViewModel SearchByUsername(string name)
        {
            return authenticationService.SearchByUsername(name);
        }

        [HttpGet("{userId}")]
        public ResponseViewModel SearchByUsername(Guid userId)
        {
            return authenticationService.getUser(userId);
        }

        [HttpDelete("{userId}")]
        public bool delete(Guid userId)
        {
            return authenticationService.deleteUser(userId);
        }

        [HttpPost("add-employer"),Authorize(Roles = "User, Admin")]
        public ResponseViewModel addEmployer(CreateEmployerModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return authenticationService.addEmployer(model,authToken);
        }

        [HttpDelete("delete-employer"),Authorize(Roles = "User, Admin")]
        public ResponseViewModel deleteEmployer(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return authenticationService.deleteEmployer(id,authToken);
        }

        [HttpPost("change-password"),Authorize()]
        public ResponseViewModel resetPassword(NewPasswordModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return authenticationService.changePassword(model,authToken);
        }
    }

}
