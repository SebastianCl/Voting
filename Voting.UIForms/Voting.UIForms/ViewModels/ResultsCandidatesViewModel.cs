namespace Voting.UIForms.ViewModels
{
    using Common.Models;
    using Helpers;

    public class ResultsCandidatesViewModel : BaseViewModel
    {
        public Event Event { get; set; }

        public string Winner { get; set; }

        public ResultsCandidatesViewModel(Event @event)
        {
            this.Event = @event;
            this.Winner = $"{Languages.Winner}: {this.GetWinner()}";
        }

        private string GetWinner()
        {
            Candidate[] myWinner = this.Event.Candidates;
            int maxVote = 0;
            var winner = "";

            foreach (var item in myWinner)
            {
                if (item.TotalVotes > maxVote)
                {
                    winner = "";
                    maxVote = item.TotalVotes;
                    winner = item.Name;
                }
                else if (item.TotalVotes == maxVote)
                {
                    winner = $"{winner}, {item.Name}";
                }
            }
            winner = $"{winner} with {maxVote} votes";
            return winner;

        }



    }
}
