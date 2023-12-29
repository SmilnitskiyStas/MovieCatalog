using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.Models.Abstract
{
    public abstract class UserAbstract
    {
        protected UserAbstract(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDay = birthDate;
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
