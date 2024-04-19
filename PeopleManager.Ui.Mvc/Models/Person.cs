using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Ui.Mvc.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First name")]
        public required string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public required string LastName { get; set; }

        [DisplayName("Email address")]
        [EmailAddress]
        public string? Email { get; set; }

        public int? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
