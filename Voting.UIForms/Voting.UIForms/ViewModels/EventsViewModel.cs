namespace Voting.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;

    public class EventsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<EventItemViewModel> events;
        private List<Event> myEvents;
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }

        public ICommand RefreshCommand => new RelayCommand(this.LoadEvents);        public ICommand SelectEventCommand => new RelayCommand(this.SelectEvent);

        private async void SelectEvent()
        {
            await App.Navigator.PushAsync(new AboutPage());
        }

        public ObservableCollection<EventItemViewModel> Events
        {
            get => this.events;
            set => this.SetValue(ref this.events, value);
        }

        public EventsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadEvents();
        }

        private async void LoadEvents()
        {
            this.IsRefreshing = true;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Event>(
                url,
                "/api",
                "/Events",
                "bearer",
            MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
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
