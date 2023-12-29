using MovieCatalog.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
