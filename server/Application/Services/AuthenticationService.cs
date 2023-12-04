using JobPortal.Application.Interfaces;
using JobPortal.Application.ViewModels.AuthM;
using JobPortal.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using JobPortal.Application.ViewModels.ResponseM;
using System.ComponentModel;
using JobPortal.Migrations;
using JobPortal.Data.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;

namespace JobPortal.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IEmployerService employerService;
        
        private bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }
            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }

        public AuthenticationService(IConfiguration configuration, IUserService userService, IRoleService roleService, IEmployerService employerService)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.roleService = roleService;
            this.employerService = employerService;
        }

        public ResponseViewModel Login(LoginModel request)
        {
            User user = userService.getUserByEmail(request.email);
            if (user == null)
            {
                return new ResponseViewModel()
                {
                    message = "User not found",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }

            if (!VerifyPasswordHash(request.password, user.passwordHash, user.passwordSalt))
            {
                return new ResponseViewModel()
                {
                    message = "Wrong username or password",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }

            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);

            var obj = new
            {
                accessToken = token,
                refreshToken = refreshToken,
            };
            return new ResponseViewModel()
            {
                message = "Başarıyla giriş yapıldı. 🚀",
                responseModel = obj,
                statusCode = 200
            };
        }

        public ResponseViewModel Register(RegisterModel model)
        {
            CreatePasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = userService.getUserByUsername(model.name);
            if (user != null)
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı adı kullanımda. 😒",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            User user1 = new User()
            {
                name = model.name,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                TokenCreated = DateTime.UtcNow,
                TokenExpires = DateTime.UtcNow.AddMinutes(10),
                roleId = roleService.getRole("User").id,
                email = model.email,
                firstName = model.firstName,
                lastName = model.lastName,
                gender = model.gender,
                photoUrl = model.photoUrl,
            };

            User user2 = userService.add(user1);

            userService.update(user2);
            Console.WriteLine(user2);

            return new ResponseViewModel()
            {
                message = "Başarıyla kayıt olundu. 😍",
                responseModel = user2,
                statusCode = 200
            };
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("name", user.name),
                new Claim(ClaimTypes.Role, roleService.get(user.roleId).name),
                new Claim("id", Convert.ToString(user.id)),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("JwtSettings:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds

            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, User user)
        {
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            userService.update(user);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(90),
                Created = DateTime.UtcNow
            };
            return refreshToken;
        }

        public ResponseViewModel RefreshToken(RefreshTokenModel model)
        {
            if (model.reftoken != null)
            {
                User user = userService.getUserByRefreshToken(model.reftoken);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Uygun olmayan yenileme tokeni. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                if (!user.RefreshToken.Equals(model.reftoken))
                {
                    return new ResponseViewModel()
                    {
                        message = "Uygun olmayan yenileme tokeni. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                else if (user.TokenExpires < DateTime.UtcNow)
                {
                    return new ResponseViewModel()
                    {
                        message = "Tokenin süresi geçmiş. 😐",
                        responseModel = new Object(),
                        statusCode = 401
                    };
                }

                string token = CreateToken(user);
                var newRefreshToken = GenerateRefreshToken();
                SetRefreshToken(newRefreshToken, user);

                var obj = new
                {
                    accessToken = token,
                    refreshToken = newRefreshToken.Token,
                };
                return new ResponseViewModel()
                {
                    message = "Token başarıyla yenilendi. 🥰",
                    responseModel = obj,
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Veri verilmedi. 😥",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel SearchByUsername(string username)
        {
            if (username != null)
            {
                User user = userService.getUserByUsername(username);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı bulunmadı. 😶",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }

                UserResponseModel userResponseModel = new UserResponseModel()
                {
                    id = user.id,
                    TokenCreated = user.TokenCreated,
                    name = user.name,
                    roleId = user.roleId,
                    TokenExpires = user.TokenExpires
                };
                return new ResponseViewModel()
                {
                    statusCode = 200,
                    message = "Kullanıcı getirildi. 🥰",
                    responseModel = userResponseModel
                };
            }
            return new ResponseViewModel()
            {
                message = "Veri girilmedi.",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel getUser(Guid id)
        {
            User user = userService.get(id);
            if (user == null)
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı bulunmadı. 😶",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }

            UserResponseModel userResponseModel = new UserResponseModel()
            {
                id = user.id,
                TokenCreated = user.TokenCreated,
                name = user.name,
                roleId = user.roleId,
                TokenExpires = user.TokenExpires,
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                gender = user.gender,
                googleUserId = user.googleUserId,
                photoUrl = user.photoUrl,
                RefreshToken = user.RefreshToken
            };
            return new ResponseViewModel()
            {
                statusCode = 200,
                message = "Kullanıcı getirildi. 🥰",
                responseModel = userResponseModel
            };

        }

        public ResponseViewModel addGoogleUser(GoogleCreateUserModel model)
        {
            User user2 = userService.getUserByGoogleId(model.googleUserId);
            if (user2 != null)
            {
                RefreshToken refreshO = GenerateRefreshToken();
                user2.TokenCreated = refreshO.Created;
                user2.TokenExpires = refreshO.Expires;
                user2.RefreshToken = refreshO.Token;
                User user3 = userService.update(user2);
                string v = CreateToken(user3);
                var obj1 = new
                {
                    accessToken = v,
                    refreshToken = user3.RefreshToken,
                };
                return new ResponseViewModel()
                {
                    message = "Kullanıcı başarıyla getirildi. 🚀",
                    responseModel = obj1,
                    statusCode = 200,
                };
            }
            User user = new User()
            {
                email = model.email,
                firstName = model.firstName,
                lastName = model.lastName,
                googleUserId = model.googleUserId,
                name = model.name,
                photoUrl = model.photoUrl,
                roleId = Guid.Parse("31c188f9-1f50-41cf-8b60-1401519d37f8"),
                RefreshToken = GenerateRefreshToken().Token,
                TokenExpires = GenerateRefreshToken().Expires,
                TokenCreated = GenerateRefreshToken().Created,
            };
            User user1 = userService.add(user);
            var obj = new
            {
                accessToken = model.refreshToken,
                refreshToken = model.refreshToken,
            };
            return new ResponseViewModel()
            {
                message = "Kullanıcı başarıyla getirildi. 🚀",
                responseModel = obj,
                statusCode = 200,
            };
        }

        public bool deleteUser(Guid id)
        {
            return userService.delete(id);
        }

        public ResponseViewModel addEmployer(CreateEmployerModel model,string authToken){
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            if (user == null){
                return new ResponseViewModel(){
                    message = "Couldnt find user.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            Employer employer = new Employer(){
                company_date_created = model.company_date_created,
                company_logo_photo_url = model.company_logo_photo_url,
                company_name = model.company_name,
                companys_job = model.companys_job,
                user_id = user.id,
            };
            ResponseViewModel responseViewModel = employerService.addEmployer(employer);
            return responseViewModel;
        }
    
        public ResponseViewModel deleteEmployer(Guid id,string authToken){
            ResponseViewModel responseViewModel = employerService.deleteEmployer(id,authToken);
            return responseViewModel;
        }

        public ResponseViewModel changePassword(NewPasswordModel model,string authToken){
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            if (user.googleUserId!=null){
                return new ResponseViewModel(){
                    message = "You logged in with google, cant change your password.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            if (user == null){
                return new ResponseViewModel(){
                    message = "Couldnt find user.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            if (model.newPassword1!=model.newPassword2){
                return new ResponseViewModel(){
                    message = "Passwords dont match.",
                    statusCode = 400,
                    responseModel = new object()
                };
            }
            CreatePasswordHash(model.newPassword1, out byte[] passwordHash, out byte[] passwordSalt);
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            User user1 = userService.update(user);
            return new ResponseViewModel(){
                message = "Password changed.",
                responseModel = user1,
                statusCode = 200
            };
        }

        public ResponseViewModel resetPasswordSendMail(ResetPasswordMailModel model){
            User user = userService.getUserByEmail(model.email);
            if (user == null){
                return new ResponseViewModel(){
                    message = "Couldnt find user.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            var builder = WebApplication.CreateBuilder();
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("besevler.mah.muh@gmail.com", builder.Configuration.GetSection("MailPassword").Value),
                EnableSsl = true,
            };

            List<Claim> claims = new List<Claim>
            {
                new Claim("id", Convert.ToString(user.id)),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("JwtSettings:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            var mailSending = new MailMessage();
            mailSending.Subject = "Password reset";
            mailSending.From = new MailAddress("besevler.mah.muh@gmail.com");
            mailSending.To.Add(user.email);
            System.Console.WriteLine(jwt);
            mailSending.Body = "http://localhost:4200/reset-password/"+Convert.ToString(jwt);
            mailSending.IsBodyHtml = true;
            smtpClient.Send(mailSending);

            return new ResponseViewModel(){
                message = "Successfully sent mail.",
                responseModel = new object(),
                statusCode = 200
            };
        }
    
        public ResponseViewModel resetPasswordCheckJwt(string jwtToken){
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken);
            var tokenS = jsonToken as JwtSecurityToken;
            if (!IsValid(jwtToken)){
                return new ResponseViewModel(){
                    message = "Token expired.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            Guid id = new Guid(tokenS.Claims.First(claim => claim.Type == "id").Value);
            User user = userService.get(id);

            if (user == null){
                return new ResponseViewModel(){
                    message = "User not found.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }

            return new ResponseViewModel(){
                message = "Successfully got user.",
                responseModel = user,
                statusCode = 200
            };
        }
    
        public ResponseViewModel resetPassword(ResetPasswordModel model){
            
            ResponseViewModel responseViewModel = resetPasswordCheckJwt(model.token);
            
            User user = (User)responseViewModel.responseModel;

            if (user.googleUserId!=null){
                return new ResponseViewModel(){
                    message = "Cant change this accounts password.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }

            if (model.newPassword1!=model.newPassword2){
                return new ResponseViewModel(){
                    message = "Passwords do not match.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }

            CreatePasswordHash(model.newPassword1,out byte[] passwordHash, out byte[] passwordSalt);


            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;

            userService.update(user);

            return new ResponseViewModel(){
                message = "Successfully changed users password.",
                responseModel = new object(),
                statusCode = 200
            };
        }
    
    }

}



