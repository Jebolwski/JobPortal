namespace JobPortal.Domain.Entities
{
    public class JobAdPhoto : Entity
    {
        public Guid jobAdId { get; set; }
        public string photoUrl { get; set; }
    }
}
