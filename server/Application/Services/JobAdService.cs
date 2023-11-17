using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Domain.Interfaces;
using JobPortal.Migrations;
using System.IdentityModel.Tokens.Jwt;

namespace JobPortal.Application.Services
{
    public class JobAdService : IJobAdService
    {

        private readonly IJobAdRepository jobAdRepository;
        private readonly IJobAdPhotoService jobAdPhotoService;
        private readonly IUserService userService;
        private readonly IEmployerService employerService;

        public JobAdService(IJobAdRepository jobAdRepository, IJobAdPhotoService jobAdPhotoService, IUserService userService, IEmployerService employerService = null)
        {
            this.jobAdRepository = jobAdRepository;
            this.jobAdPhotoService = jobAdPhotoService;
            this.userService = userService;
            this.employerService = employerService;
        }

        public ResponseViewModel addJobAd(CreateJobAdModel model,string authToken){
            
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            ResponseViewModel responseViewModel = employerService.getEmployer(user.id);
            if (responseViewModel.statusCode==400){
                return new ResponseViewModel(){
                    message = "You are not an employer.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            JobAd jobAd = new JobAd(){
                employer_id = ((Employer)responseViewModel.responseModel).id,
                creator_id = user.id,
                description = model.description,
                title = model.title,
            };
            
            JobAd jobAd1 = jobAdRepository.add(jobAd);

            foreach (CreateJobAdPhotoModel photo in model.photos)
            {
                JobAdPhoto jobAdPhoto = new JobAdPhoto(){
                    jobAdId = jobAd1.id,
                    photoUrl = photo.photoUrl
                };

                jobAdPhotoService.addJobAdPhoto(jobAdPhoto);
            }

            JobAd jobAd2 = jobAdRepository.getJobAdWithPhotos(jobAd1.id);
            

            return new ResponseViewModel(){
                statusCode = 200,
                message = "Successfully added job ad.",
                responseModel = jobAd2
            };
        }

        public ResponseViewModel deleteJobAd(Guid id,string authToken){
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            JobAd jobAd = jobAdRepository.get(id);
            ResponseViewModel responseViewModel = employerService.getEmployer(user.id);
            if (responseViewModel.statusCode==400){
                return new ResponseViewModel(){
                    message = "You are not an employer.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            if (user.id == jobAd.creator_id){
                bool v = jobAdRepository.delete(id);
                if (v){
                    return new ResponseViewModel(){
                        message = "Successfully deleted job ad.",
                        statusCode = 200,
                        responseModel = new object()
                    };
                }
                return new ResponseViewModel(){
                    message = "Failed to delete job ad.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            return new ResponseViewModel(){
                    message = "You dont own this job ad.",
                    responseModel = new object(),
                    statusCode = 400
            };
        }

        public ResponseViewModel updateJobAd(UpdateJobAdModel model,string authToken){
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            JobAd jobAd2 = jobAdRepository.getJobAdWithPhotos(model.id);
            ResponseViewModel responseViewModel = employerService.getEmployer(user.id);
            if (responseViewModel.statusCode==400){
                return new ResponseViewModel(){
                    message = "You are not an employer.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            if (user.id!=jobAd2.creator_id){
                return new ResponseViewModel(){
                    message = "You dont own this job ad.",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
            
            // jobadphotoları sil
            foreach (JobAdPhoto adPhoto in jobAd2.photos)
            {
                jobAdPhotoService.deleteJobAdPhoto(adPhoto.id);
            }
            // yenilerini ekle
            foreach (CreateJobAdPhotoModel createJobAdPhotoModel in model.photos)
            {
                
                jobAdPhotoService.addJobAdPhoto(new JobAdPhoto(){
                    jobAdId = model.id,
                    photoUrl = createJobAdPhotoModel.photoUrl
                });
            }
            jobAd2.creator_id = user.id;
            jobAd2.title = model.title;
            jobAd2.description = model.description;
            JobAd jobAd1 = jobAdRepository.update(jobAd2);
            if (jobAd1!=null){
                return new ResponseViewModel(){
                    message = "Successfully updated job ad.",
                    statusCode = 200,
                    responseModel = jobAd1
                };
            }
            return new ResponseViewModel(){
                message = "Failed to update job ad.",
                responseModel = new object(),
                statusCode = 400
            };
        }

        public ResponseViewModel getJobAd(Guid id){
            JobAd jobAd = jobAdRepository.getJobAdWithPhotos(id);
            
            if (jobAd!=null){
                return new ResponseViewModel(){
                    message = "Successfully got job ad.",
                    statusCode = 200,
                    responseModel = jobAd
                };
            }
            return new ResponseViewModel(){
                message = "Failed to got job ad.",
                responseModel = new object(),
                statusCode = 400
            };
        }
    
    }

}



