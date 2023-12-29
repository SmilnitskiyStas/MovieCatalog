using MovieCatalog.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models
{
    public class Producer
    {
        [Key]
        public int ProducerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public ICollection<MovieProducer> MovieProducers { get; set; }
    }
}
