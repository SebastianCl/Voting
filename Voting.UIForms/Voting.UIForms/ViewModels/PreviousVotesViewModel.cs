namespace Voting.UIForms.ViewModels
{
    using System;
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
        private List<Vote> myVotes;
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

            this.myVotes = (List<Vote>)response.Result;
            

            //this.myEvents = (List<Event>)response.Result;
            if (this.myVotes == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.Error,
                    Languages.Accept);
                this.IsRefreshing = false;
                return;
            }
            this.RefresEventsList();
            this.IsRefreshing = false;
        }

        
        private void RefresEventsList()
        {
            //TODO: Fix previous vote
            this.Events = new ObservableCollection<EventItemViewModel>(
                this.myVotes.Select(u => new EventItemViewModel
                {
                    Id = u.Id,
                    Name = u.Event.Name,
                    Description = u.Event.Description,
                    StartDate = u.Event.StartDate,
                    FinishDate = u.Event.FinishDate,
                    Candidates = u.Event.Candidates,
                    NumberCandidates = u.Event.NumberCandidates
                })
            .OrderBy(u => u.Name)
            .ToList());
        }


    }
}
