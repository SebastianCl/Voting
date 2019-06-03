namespace Voting.Common.ViewModels
{
    using Services;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;

    public class EventsDetailViewModel : MvxViewModel<NavigationArgs>
    {
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private Event @event;
        private bool isLoading;
        private MvxCommand<Candidate> itemClickCommand;

        public EventsDetailViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.IsLoading = false;
        }

        public ICommand ItemClickCommand
        {
            get
            {
                this.itemClickCommand = new MvxCommand<Candidate>(this.OnItemClickCommand);
                return itemClickCommand;
            }
        }
               

        private async void OnItemClickCommand(Candidate candidate)
        {
            var request = new NewVote
            {
                Email = Settings.UserEmail,
                Candidate = candidate.Id,
                Event = Event.Id
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.RegisterVoteAsync(
                "https://uvoting.azurewebsites.net",
                "/api",
                "/Votes",
                request,
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert(
                    "Information",
                    response.Message,
                    "Accept");
                return;
            }

            this.dialogService.Alert("Ok", 
                "Your vote for " + candidate.Name + " was saved", 
                "Accept");
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

       
        public override void Prepare(NavigationArgs parameter)
        {
            this.@event = parameter.Event;
        }

    }

}
