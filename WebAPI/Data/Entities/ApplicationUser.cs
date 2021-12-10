using Microsoft.AspNetCore.Identity;

namespace WebAPI.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string MobileNumber { get; set; }
        public string PictureURI { get; set; }
        public bool IsOnline { get; set; }
        public int TabsOpen { get; set; }
        public bool IsInCall { get; set; }
    }
}
