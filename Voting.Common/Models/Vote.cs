namespace Voting.Common.Models
{
    using Newtonsoft.Json;

    public class Vote
    {
        public int Id { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("candidates")]
        public Candidate[] Candidate { get; set; }

        [JsonProperty("event")]
        public Event[] Event { get; set; }
    }
}
