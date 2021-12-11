using System;

namespace WebAPI.Data.Entities
{
    public class LearningNote
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string LearningText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
