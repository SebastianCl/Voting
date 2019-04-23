namespace Voting.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class CandidateItemViewModel : Candidate
    {

        public ICommand SelectCandidateCommand => new RelayCommand(this.SelectCandidate);


        private async void SelectCandidate()
        {
            await Application.Current.MainPage.DisplayAlert(
                    "Titulo",
                    "Hola",
                    "Aceptar");
        }


       
    }
}
