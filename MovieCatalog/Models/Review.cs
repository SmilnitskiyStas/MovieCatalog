using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public Reviewer Reviewer { get; set; }
        public Movie Movie { get; set; }
    }
}
