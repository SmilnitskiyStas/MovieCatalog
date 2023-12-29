using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }
        public ICollection<MovieCountry> MovieCountries { get; set; }
    }
}
