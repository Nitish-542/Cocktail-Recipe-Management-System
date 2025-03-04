using System.ComponentModel.DataAnnotations;
namespace CRM.Models
{
    public class Bartender
    {
        [Key]
        public int BartenderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DrinksPosted { get; set; }
        public string Email { get; set; }
        public DateTime LastPosted { get; set; }
    }
    public class BartenderDto
    {
        public int BartenderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DrinksPosted { get; set; }
        public string Email { get; set; }
        public DateTime LastPosted { get; set; }

        // Computed Full Name property
        public string FullName => $"{FirstName} {LastName}";
    }

}
