namespace Voting.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Views;

    public class EventItemViewModel : Event
    {
        public ICommand SelectEventCommand => new RelayCommand(this.SelectEvent);

        private async void SelectEvent()
        {
            MainViewModel.GetInstance().Vote = new VoteViewModel((Event)this);
            await App.Navigator.PushAsync(new VotePage());
        }
    }

}

