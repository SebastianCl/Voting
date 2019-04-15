namespace Voting.Common.Models
{
    using System;
    using Newtonsoft.Json;

    public class Event
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("finishDate")]
        public DateTime FinishDate { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("candidates")]
        public Candidate[] Candidates { get; set; }

        [JsonProperty("numberCandidates")]
        public int NumberCandidates { get; set; }

        public string NumberCandidatesText
        {
            get
            {
               return $"Number of candidates: {Convert.ToString(this.NumberCandidates)}";
            }
        }

    }
}
