namespace Voting.Common.Models
{
    using Newtonsoft.Json;

    public class Vote
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("candidate")]
        public Candidate Candidate { get; set; }

        [JsonProperty("event")]
        public Event Event { get; set; }
    }
}
