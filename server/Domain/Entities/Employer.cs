namespace JobPortal.Domain.Entities
{
    public class Employer : Entity
    {
        public Guid user_id { get; set; }
        public string company_name { get; set; }
        public string companys_job { get; set; }
        public DateTime company_date_created { get; set; }
        public string company_logo_photo_url { get; set; }
    }
}
