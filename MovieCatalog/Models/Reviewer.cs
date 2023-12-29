using MovieCatalog.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models
{
    public class Reviewer
    {
        [Key]
        public int ReviewerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
