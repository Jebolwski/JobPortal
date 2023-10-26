namespace JobPortal.Application.ViewModels.AuthM
{
    public class UserResponseModel
    {
        public Guid id { get; set; }
        public Guid roleId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string googleUserId { get; set; }
        public bool gender { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string photoUrl { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
