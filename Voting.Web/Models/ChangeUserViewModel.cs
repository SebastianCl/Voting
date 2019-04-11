namespace Voting.Web.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }     

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Occupation { get; set; }

        [Required]
        [Range(1, 6, ErrorMessage = "The {0} must be between {1} and {2}.")]
        public int Stratum { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Gender { get; set; }        

        [Required]
        public DateTime Birthdate { get; set; }

        [Display(Name = "City")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a city.")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country.")]
        public int CountryId { get; set; }        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
