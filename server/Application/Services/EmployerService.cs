using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Domain.Interfaces;
using JobPortal.Application.ViewModels.AuthM;
using JobPortal.Migrations;
using System.IdentityModel.Tokens.Jwt;

namespace JobPortal.Application.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository EmployerRepository;
        private readonly IUserService UserService;

        public EmployerService(IEmployerRepository employerRepository, IUserService userService)
        {
            EmployerRepository = employerRepository;
            UserService = userService;
        }

        public ResponseViewModel addEmployer(Employer model)
        {
            Employer employer = EmployerRepository.add(model);
            if (employer == null){
                return new ResponseViewModel(){
                    message = "Could'nt add employer successfully.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            return new ResponseViewModel(){
                message = "Successfully added employer.",
                responseModel = employer,
                statusCode = 200
            };

        }

        public ResponseViewModel deleteEmployer(Guid id,string authToken)
        {
            Employer employer = EmployerRepository.get(id);
            if  (employer==null){
                return new ResponseViewModel(){
                    message = "Couldnt find the employer you want to delete.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            bool v = EmployerRepository.delete(id);
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = UserService.getUserByUsername(jsonToken.Claims.First().Value);
            if (user.id==employer.user_id){
                if (v){
                    return new ResponseViewModel(){
                        message = "Successfully deleted employer.",
                        responseModel = new object(),
                        statusCode = 200
                    };
                }
                return new ResponseViewModel(){
                    message = "Couldnt delete employer successfully.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            return new ResponseViewModel(){
                message = "You are not the employer.",
                responseModel = new object(),
                statusCode = 400
            };
        }
    }

}



