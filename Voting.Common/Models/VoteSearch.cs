namespace Voting.Common.Models
{
    using Newtonsoft.Json;

    public class VoteSearch
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("event")]
        public int Event { get; set; }

        [JsonProperty("candidate")]
        public int Candidate { get; set; }

    }
}
