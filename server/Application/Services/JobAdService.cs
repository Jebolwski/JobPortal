using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.Application.ViewModels.ResponseM;
using JobPortal.Domain.Interfaces;

namespace JobPortal.Application.Services
{
    public class JobAdService : IJobAdService
    {

        private readonly IJobAdRepository jobAdRepository;
        private readonly IJobAdPhotoService jobAdPhotoService;

        public JobAdService(IJobAdRepository jobAdRepository, IJobAdPhotoService jobAdPhotoService)
        {
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
                jobAdPhotoService.addJobAdPhoto(createJobAdPhotoModel);
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



