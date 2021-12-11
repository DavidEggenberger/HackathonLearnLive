using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Group
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string Question { get; set; }
    }
}
