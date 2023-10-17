using System.Net;

namespace JobPortal.Domain.Entities
{
    public class User : Entity
    {
        public Guid roleId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string googleUserId { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string photoUrl { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
