using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; }

        //Relationships

        // One-To-Many
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Image> Images { get; set; }
        // Many-To-Many
        public ICollection<MovieCategory> MovieCategories { get; set; }
        public ICollection<MovieProducer> MovieProducers { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<MovieCountry> MovieCountries { get; set; }
    }
}
