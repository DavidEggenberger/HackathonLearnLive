using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebAPI.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string MobileNumber { get; set; }
        public string PictureURI { get; set; }
        public bool IsOnline { get; set; }
        public int TabsOpen { get; set; }
        public bool IsInCall { get; set; }
        public List<ApplicationUserGroupMembership> JoinedGroups { get; set; }
        public List<GroupMessage> SentMessages { get; set; }
    }
}
