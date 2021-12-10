using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Entities
{
    public class GroupUserMembership
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
