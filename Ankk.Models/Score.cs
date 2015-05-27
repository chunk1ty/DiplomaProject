namespace Ankk.Models
{
    public class Score
    {
        public int Id { get; set; }

        public int Points { get; set; }

        public int SubjectId { get; set; }

        public virtual  Subject Subject { get; set; }
    }
}
