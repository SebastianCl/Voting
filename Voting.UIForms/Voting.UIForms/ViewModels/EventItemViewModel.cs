namespace Voting.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Views;
    using Voting.Common.Services;
    using Xamarin.Forms;

    public class EventItemViewModel : Event
    {        
        public ICommand SelectEventCommand => new RelayCommand(this.SelectEvent);

        public ICommand SelectResultCommand => new RelayCommand(this.SelectResult);

        public string Winner { get; set; }

        public string StartDateText
        {
            get
            {
                return $"{Languages.StartDate}: {this.StartDate}";
            }
        }
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
            MainViewModel.GetInstance().Candidate = new CandidateViewModel((Event)this);
            await App.Navigator.PushAsync(new CandidatePage());
        }
        
        private async void SelectResult()
        {
            MainViewModel.GetInstance().ResultsCandidates = new ResultsCandidatesViewModel((Event)this);
            await App.Navigator.PushAsync(new ResultsCandidatesPage());
        }
        

    }

}

