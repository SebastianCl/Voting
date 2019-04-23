namespace Voting.UIForms.ViewModels
{
    using Common.Models;
    using Common.Services;

    public class CandidateViewModel : BaseViewModel
    {
        private readonly ApiService apiService;

        public Event Event { get; set; }

        public string Winner { get; set; }


        public CandidateViewModel(Event @event)
        {
            this.Event = @event;
            this.apiService = new ApiService();
        }

    }
}
