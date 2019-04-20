namespace Voting.UIForms.ViewModels
{
    using System;
    using System.Windows.Input;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;

    public class VoteViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

        public Event Event { get; set; }

        public ICommand RefreshCommand => new RelayCommand(this.LoadCandidates);

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public VoteViewModel(Event @event)
        {
            this.Event = @event;
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        private void LoadCandidates()
        {
            throw new NotImplementedException();
        }


    }
}
