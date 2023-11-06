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

        public JobAdService(IJobAdRepository jobAdRepository, IJobAdPhotoService jobAdPhotoService, IUserService userService)
        {
            this.jobAdRepository = jobAdRepository;
            this.jobAdPhotoService = jobAdPhotoService;
            this.userService = userService;
        }

        public ResponseViewModel addJobAd(CreateJobAdModel model,string authToken){
            
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);

            JobAd jobAd = new JobAd(){
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
            

            return new ResponseViewModel(){
                statusCode = 200,
                message = "Successfully added job ad.",
                responseModel = jobAd1
            };
        }

        public ResponseViewModel deleteJobAd(Guid id){
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

        public ResponseViewModel updateJobAd(UpdateJobAdModel model,Guid id){
            JobAd jobAd = new JobAd(){
                creator_id = model.creator_id,
                description = model.description,
                id = id,
                title = model.title,
            };
            JobAd jobAd2 = jobAdRepository.get(id);

            // jobadphotoları sil
            foreach (JobAdPhoto adPhoto in jobAd2.photos)
            {
                jobAdPhotoService.deleteJobAdPhoto(adPhoto.id);
            }
            // yenilerini ekle
            foreach (CreateJobAdPhotoModel createJobAdPhotoModel in model.photos)
            {
                
                jobAdPhotoService.addJobAdPhoto(new JobAdPhoto(){
                    jobAdId = id,
                    photoUrl = createJobAdPhotoModel.photoUrl
                });
            }
            JobAd jobAd1 = jobAdRepository.update(jobAd);
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
    
    }

}



