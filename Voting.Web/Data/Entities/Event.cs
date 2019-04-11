namespace Voting.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event : IEntity
    {
        public int Id { get; set; }

        [Required]        
        [Display(Name = "Event Name")]
        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters length.")]        
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Finish Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FinishDate { get; set; }
        
        public User User { get; set; }

        public ICollection<Candidate> Candidates { get; set; }

        [Display(Name = "# Candidates")]
        public int NumberCandidates { get { return this.Candidates == null ? 0 : this.Candidates.Count; } }
    }
}
