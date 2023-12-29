using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models.Dto
{
    public class ActorDto
    {
        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
