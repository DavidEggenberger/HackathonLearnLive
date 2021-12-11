using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Purpose { get; set; }
        public string PictureURI { get; set; }
        public string CreatorApplicationUserId { get; set; }
        public List<GroupUserMembership> ApplicationUsersInGroup { get; set; }
        public List<GroupMessage> Messages { get; set; }
    }
}
