namespace Voting.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Event : IEntity
    {
        public int Id { get; set; }

        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Display(Name = "Event Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "Finish Date")]
        [Required]
        public DateTime FinishDate { get; set; }
        
        public User User { get; set; }
    }
}
