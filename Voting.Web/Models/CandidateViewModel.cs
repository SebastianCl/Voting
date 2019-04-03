namespace Voting.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;
    using Microsoft.AspNetCore.Http;    

    public class CandidateViewModel : Candidate
    {
        public int EventId { get; set; }
        public int CandidateId { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
