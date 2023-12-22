using JobPortal.Application.Interfaces;
using JobPortal.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using JobPortal.Controllers;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Application.ViewModels.AuthM;
using FluentAssertions;
using JobPortal.Domain.Entities;

namespace JobPortal.Test.ServiceTests
{
    public class AuthenticationServiceTest
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IEmployerService employerService;

        public AuthenticationServiceTest()
        {
            this.userService = A.Fake<IUserService>();
            this.roleService = A.Fake<IRoleService>();
            this.employerService = A.Fake<IEmployerService>();
            this.authenticationService = A.Fake<IAuthenticationService>();
        }

        [Fact]
        public void AuthenticationController_Login_ReturnsResponseViewModel()
        {
            var response = A.Fake<ResponseViewModel>();
            var model = A.Fake<LoginModel>();
            var user = A.Fake<User>();
            var email = "savdasd";
            A.CallTo(() =>  userService.getUserByEmail(email)).Returns(user);
            var result = authenticationService.Login(model);

            result.Should().BeOfType<ResponseViewModel>();
        }

    }
}
