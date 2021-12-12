using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Group
{
    public class LearningNoteDTO
    {
        public string GroupName { get; set; }
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string LearningMessage { get; set; }
    }
}
