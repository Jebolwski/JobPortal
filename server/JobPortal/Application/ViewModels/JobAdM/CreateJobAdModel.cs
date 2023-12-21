using JobPortal.Domain.Entities;

namespace JobPortal.Application.ViewModels.ResponseM
{
    public class CreateJobAdModel
    {
       public string title { get; set; }
        public string description { get; set; }
        public ICollection<CreateJobAdPhotoModel> photos { get; set; }
    }
}