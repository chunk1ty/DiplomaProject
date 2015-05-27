namespace Ankk.Models
{
    using System.Collections.Generic;

    public class Subject
    {
        public Subject()
        {            
            this.Scores = new HashSet<Score>();
            this.Users = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int TeacherId { get; set; }

        public virtual ICollection<Score> Scores { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
