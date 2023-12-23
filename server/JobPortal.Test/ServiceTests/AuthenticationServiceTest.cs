﻿using JobPortal.Application.Interfaces;
using FakeItEasy;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Application.ViewModels.AuthM;
using FluentAssertions;
using JobPortal.Domain.Entities;
using JobPortal.Migrations;

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
            var email = "mertogoko4@gmail.com";
            A.CallTo(() =>  userService.getUserByEmail(email)).Returns(user);

            var result = authenticationService.Login(model);
            result.Should().NotBeNull();
            
        }

        [Fact]
        public void AuthenticationController_Register_ReturnsResponseViewModel()
        {
            var response = A.Fake<ResponseViewModel>();
            var model = A.Fake<RegisterModel>();
            var result = authenticationService.Register(model);
            result.Should().NotBeNull();
            result.statusCode.Should().Be(200);
        }

    }
}
