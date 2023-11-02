using JobPortal.Domain.Entities;

namespace JobPortal.Application.ViewModels.ResponseM
{
    public class CreateJobAdPhotoModel
    {
        public Guid jobAdId { get; set; }
        public string photoUrl { get; set; }
    }
}