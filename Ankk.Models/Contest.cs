namespace Ankk.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Contest
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsVisible { get; set; }
    }
}
