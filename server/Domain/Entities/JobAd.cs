namespace JobPortal.Domain.Entities
{
    public class JobAd:Entity
    {
        public string title { get; set; }
        public string description { get; set; }
        public ICollection<JobAdPhoto> photos { get; set; }
        public Guid creator_id { get; set; }
    }
}
