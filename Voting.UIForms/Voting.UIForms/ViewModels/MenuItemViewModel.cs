﻿namespace Voting.UIForms.ViewModels
{
    using System.Windows.Input;
    using Common.Helpers;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;

    public class MenuItemViewModel : Common.Models.Menu
    {
        public ICommand SelectMenuCommand => new RelayCommand(this.SelectMenu);

        private async void SelectMenu()
        {
            App.Master.IsPresented = false;
            var mainViewModel = MainViewModel.GetInstance();
            switch (this.PageName)
            {
                case "AboutPage":
                    await App.Navigator.PushAsync(new AboutPage());
                    break;
                case "ProfilePage":
                    mainViewModel.Profile = new ProfileViewModel();
                    await App.Navigator.PushAsync(new ProfilePage());
                    break;
                case "PreviousVotesPage":
                    mainViewModel.PreviousVotes = new PreviousVotesViewModel();
                    await App.Navigator.PushAsync(new PreviousVotesPage());
                    break;
                default:
                    Settings.IsRemember = false;
                    Settings.UserEmail = string.Empty;
                    Settings.UserPassword = string.Empty;
                    Settings.Token = string.Empty;
                    Settings.User = string.Empty;
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }

    }
}
