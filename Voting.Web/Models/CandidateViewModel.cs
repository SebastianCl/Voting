namespace Voting.Web.Models
{
    using Data.Entities;
    using System.ComponentModel.DataAnnotations;

    public class CandidateViewModel : Candidate
    {
        public int EventId { get; set; }
        public int CandidateId { get; set; }

    }
}
