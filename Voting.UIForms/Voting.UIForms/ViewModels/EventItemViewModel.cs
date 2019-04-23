namespace Voting.UIForms.ViewModels
{
    using System;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Views;

    public class EventItemViewModel : Event
    {
        public ICommand SelectEventCommand => new RelayCommand(this.SelectEvent);

        public string FinishDateText
        {
            get
            {
                return $"{Languages.FinishDate}: {this.FinishDate}";
            }
        }

        public string NumberCandidatesText
        {
            get
            {
                return $"{Languages.NumberOfCandidates}: {Convert.ToString(this.NumberCandidates)}";
            }
        }


        private async void SelectEvent()
        {
            MainViewModel.GetInstance().Vote = new VoteViewModel((Event)this);
            await App.Navigator.PushAsync(new VotePage());
        }
    }

}

