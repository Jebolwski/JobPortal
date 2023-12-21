using JobPortal.Application.ViewModels.AuthM;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Application.Interfaces
{
    public interface IAuthenticationService
    {
        public ResponseViewModel Register(RegisterModel model);
        public ResponseViewModel Login(LoginModel request);
        public ResponseViewModel RefreshToken(RefreshTokenModel model);
        public ResponseViewModel SearchByUsername(string name);
        public ResponseViewModel getUser(Guid id);
        public ResponseViewModel addGoogleUser(GoogleCreateUserModel model);
        public bool deleteUser(Guid id);
        public ResponseViewModel addEmployer(CreateEmployerModel model,string authToken);
        public ResponseViewModel deleteEmployer(Guid id,string authToken);
        public ResponseViewModel changePassword(NewPasswordModel model,string authToken);
        public ResponseViewModel resetPasswordSendMail(ResetPasswordMailModel model);
        public ResponseViewModel resetPasswordCheckJwt(string jwtToken);
        public ResponseViewModel resetPassword(ResetPasswordModel model);
    }
}
