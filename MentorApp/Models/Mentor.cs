using System;

namespace MentorApp.Models
{
    public class Mentor
    {
        private MentorContext context;
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
