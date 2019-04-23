namespace Voting.Web.Models
{
    using Data.Entities;

    public class ResultViewModel : Event
    {
        public string Winner { get; set; }
    }
}
