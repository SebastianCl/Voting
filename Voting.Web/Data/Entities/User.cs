namespace Voting.Web.Data.Entities
{    
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Required]
        public string LastName { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        public string Occupation { get; set; }

        [Required]
        public int Stratum { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

    }
}
