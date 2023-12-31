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


        public ResponseViewModel addJobAdPhoto(JobAdPhoto photo){
            JobAdPhoto jobAdPhoto = jobAdPhotoRepository.add(photo);

            return new ResponseViewModel(){
                statusCode = 200,
                responseModel = jobAdPhoto,
                message = "Job ad photo added successfully."
            };
        }
    
        public ResponseViewModel deleteJobAdPhoto(Guid id){
            bool v = jobAdPhotoRepository.delete(id);
            if (v){
                return new ResponseViewModel(){
                    message = "Successfully deleted job ad photo.",
                    statusCode = 200,
                    responseModel = new object()
                };
            }
            return new ResponseViewModel(){
                message = "Failed to delete job ad photo.",
                responseModel = new object(),
                statusCode = 400
            };
        }
    

    }

}



