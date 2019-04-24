namespace Voting.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Voting.Common.Helpers;
    using Xamarin.Forms;

    public class PreviousVotesViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<EventItemViewModel> events;
        private List<Event> myEvents;
        private bool isRefreshing;
        public string Winner { get; set; }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }

        public ICommand RefreshCommand => new RelayCommand(this.LoadEvents);

        public ObservableCollection<EventItemViewModel> Events
        {
            get => this.events;
            set => this.SetValue(ref this.events, value);
        }

        public PreviousVotesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadEvents();
        }

        private async void LoadEvents()
        {
            this.IsRefreshing = true;
            string email = MainViewModel.GetInstance().UserEmail;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetPreviousVotesAsync(
                url,
                "/api",
                "/Votes/User",
                email,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                this.IsRefreshing = false;
                return;
            }

            this.myEvents = (List<Event>)response.Result;
            this.RefresEventsList();
            this.IsRefreshing = false;
        }

        private void RefresEventsList()
        {
            this.Events = new ObservableCollection<EventItemViewModel>(
                this.myEvents.Select(u => new EventItemViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    StartDate = u.StartDate,
                    FinishDate = u.FinishDate,
                    Candidates = u.Candidates,
                    NumberCandidates = u.NumberCandidates
                })
            .OrderBy(u => u.Name)
            .ToList());
        }


    }
}
