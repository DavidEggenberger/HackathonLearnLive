using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Entities
{
    public class GroupMessage
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser SenderApplicationUser { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public string TextMessage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
