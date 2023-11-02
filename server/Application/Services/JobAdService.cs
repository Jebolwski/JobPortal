using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Application.Services
{
    public class JobAdService : IJobAdService
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IJobAdRepository jobAdRepository;
        private readonly IJobAdPhotoService jobAdPhotoService;

        public JobAdService(IConfiguration configuration, IUserService userService, IRoleService roleService, IJobAdRepository jobAdRepository, IJobAdPhotoService jobAdPhotoService)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.roleService = roleService;
            this.jobAdRepository = jobAdRepository;
            this.jobAdPhotoService = jobAdPhotoService;
        }

        public ResponseViewModel addJobAd(CreateJobAdModel model){
            JobAd jobAd = new JobAd(){
                creator_id = model.creator_id,
                description = model.description,
                title = model.title,
            };

            foreach (JobAdPhoto photo in model.photos)
            {
                JobAdPhoto jobAdPhoto = new JobAdPhoto(){
                    jobAdId = photo.jobAdId,
                    photoUrl = photo.photoUrl
                };

                jobAdPhotoService.addJobAdPhoto(new CreateJobAdPhotoModel(){
                    jobAdId = jobAdPhoto.jobAdId,
                    photoUrl = jobAdPhoto.photoUrl
                });
            }
            
            JobAd jobAd1 = jobAdRepository.add(jobAd);

            return new ResponseViewModel(){
                statusCode = 200,
                message = "Successfully added job ad.",
                responseModel = jobAd1
            };
        }
    
    }

}



