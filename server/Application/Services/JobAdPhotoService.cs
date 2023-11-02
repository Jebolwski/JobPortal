using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Application.Services
{
    public class JobAdPhotoService : IJobAdPhotoService
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IJobAdPhotoRepository jobAdPhotoRepository;

        public JobAdPhotoService(IConfiguration configuration, IUserService userService, IRoleService roleService, IJobAdPhotoRepository jobAdPhotoRepository)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.roleService = roleService;
            this.jobAdPhotoRepository = jobAdPhotoRepository;
        }


        public ResponseViewModel addJobAdPhoto(CreateJobAdPhotoModel model){
            JobAdPhoto jobAdPhoto = jobAdPhotoRepository.add(new JobAdPhoto(){
                jobAdId = model.jobAdId,
                photoUrl = model.photoUrl
            });

            return new ResponseViewModel(){
                statusCode = 200,
                responseModel = jobAdPhoto,
                message = "Job ad photo added successfully."
            };
        }
    
    }

}



