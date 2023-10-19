namespace JobPortal.Application.ViewModels.AuthM
{
    public class GoogleCreateUserModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string photoUrl { get; set; }
        public string googleUserId { get; set; }
        public string refreshToken { get; set; }
    }
}
