namespace Voting.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class CandidateViewModel : BaseViewModel
    {
        private bool IsRefreshing;
        private readonly ApiService apiService;

        public Event Event { get; set; }

        public ICommand RefreshCommand => new RelayCommand(this.LoadCandidates);

        public ICommand SelectCandidateCommand => new RelayCommand(this.SelectCandidate);

        public bool IsRunning
        {
            get => this.IsRefreshing;
            set => this.SetValue(ref this.IsRefreshing, value);
        }


        public CandidateViewModel(Event @event)
        {
            this.Event = @event;
            this.apiService = new ApiService();
        }


        private async void SelectCandidate()
        {
            await Application.Current.MainPage.DisplayAlert(
                    "Titulo",
                    "Hola",
                    "Aceptar");
        }


        private async void LoadCandidates()
        {
            await Application.Current.MainPage.DisplayAlert(
                    "Titulo",
                    "Hola",
                    "Aceptar");
        }


    }
}
