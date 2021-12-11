using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Group
{
    public class LearningNoteDTO
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string Message { get; set; }
    }
}
