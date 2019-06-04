namespace Voting.Common.ViewModels
{
    using Models;
    using MvvmCross.ViewModels;

    public class ResultsDetailViewModel : MvxViewModel<NavigationArgs>
    {
        private Event @event;
        private bool isLoading;
        private string winner;

        public ResultsDetailViewModel()
        {
            this.IsLoading = false;            
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public Event Event
        {
            get => this.@event;
            set => this.SetProperty(ref this.@event, value);
        }

        public string Winner
        {
            get => this.winner;
            set => this.SetProperty(ref this.winner, value);
        }



        public override void Prepare(NavigationArgs parameter)
        {
            this.@event = parameter.Event;
            this.Winner = $"Winner: {this.GetWinner()}";
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
