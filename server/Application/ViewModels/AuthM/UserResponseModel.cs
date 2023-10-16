namespace JobPortal.Application.ViewModels.AuthM
{
    public class UserResponseModel
    {
        public Guid id { get; set; }
        public Guid roleId { get; set; }
        public string name { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
