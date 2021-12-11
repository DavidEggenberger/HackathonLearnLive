using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Group
{
    public class ChatItemDTO
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string Type { get; set; }
        public QuestionDTO QuestionDTO { get; set; }
        public LearningNoteDTO LearningNoteDTO { get; set; }
        public MessagesDTO MessagesDTO { get; set; }
    }
}
