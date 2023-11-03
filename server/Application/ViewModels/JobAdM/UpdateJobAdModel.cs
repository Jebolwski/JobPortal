using JobPortal.Domain.Entities;

namespace JobPortal.Application.ViewModels.ResponseM
{
    public class UpdateJobAdModel
    {
       public string title { get; set; }
        public string description { get; set; }
        public Guid creator_id { get; set; }
        public ICollection<CreateJobAdPhotoModel> photos { get; set; }

    }
}