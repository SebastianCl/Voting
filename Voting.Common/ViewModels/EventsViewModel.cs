namespace Voting.Common.ViewModels
{
    using Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;

    public class EventsViewModel : MvxViewModel
    {
        private List<Event> events;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private MvxCommand<Event> itemClickCommand;
        private MvxCommand resultsCommand;
        private MvxCommand editProfileCommand;

        public EventsViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }

        public ICommand ItemClickCommand
        {
            get
            {
                this.itemClickCommand = new MvxCommand<Event>(this.OnItemClickCommand);
                return itemClickCommand;
            }
        }

        public ICommand ResultsCommand
        {
            get
            {
                this.resultsCommand = new MvxCommand(this.GoToResultsCommand);
                return resultsCommand;
            }
        }

        public ICommand EditProfileCommand
        {
            get
            {
                this.editProfileCommand = new MvxCommand(this.GoToEditProfileCommand);
                return editProfileCommand;
            }
        }

        public List<Event> Events
        {
            get => this.events;
            set => this.SetProperty(ref this.events, value);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            this.LoadEventss();
        }

        private async void GoToResultsCommand()
        {
            await this.navigationService.Navigate<ResultsViewModel>();
        }

        private async void GoToEditProfileCommand()
        {
            await this.navigationService.Navigate<EditProfileViewModel>();
        }


        private async void OnItemClickCommand(Event @event)
        {
            await this.navigationService.Navigate<EventsDetailViewModel,
                NavigationArgs>(new NavigationArgs { Event = @event });
        }


        private async void LoadEventss()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.GetListAsync<Event>(
                "https://uvoting.azurewebsites.net",
                "/api",
                "/Events",
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Events = (List<Event>)response.Result;
            this.Events = this.Events.OrderBy(e => e.FinishDate).ToList();
        }

    }
}