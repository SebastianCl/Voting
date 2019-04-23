namespace Voting.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Views;

    public class MainViewModel
    {
        private static MainViewModel instance;//singleton(apuntador a la misma clase)

        public LoginViewModel Login { get; set; }

        public EventsViewModel Events { get; set; }

        public TokenResponse Token { get; set; }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }        public RegisterViewModel Register { get; set; }

        public RememberPasswordViewModel RememberPassword { get; set; }        public ChangePasswordViewModel ChangePassword { get; set; }        public CandidateViewModel Candidate { get; set; }

        public ProfileViewModel Profile { get; set; }
        
        public ResultsViewModel Results { get; set; }

        public ResultsCandidatesViewModel ResultsCandidates { get; set; }

        public User User { get; set; }

        public string UserEmail { get; set; }        public string UserPassword { get; set; }

        public MainViewModel()
        {
            instance = this;
            this.LoadMenus();
        }

        private void LoadMenus()
        {
            var menus = new List<Menu>{

                new Menu
                {
                    Icon = "ic_edit",
                    PageName = "ProfilePage",
                    Title = Languages.Profile
                },

                new Menu
                {
                    Icon = "ic_info_outline",
                    PageName = "AboutPage",
                    Title = Languages.About
                },

                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = Languages.CloseSession
                }
            };

            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title
            }).ToList());
        }

        //Singleton
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        public ICommand ResultsCommand { get { return new RelayCommand(this.GoResults); } }

        private async void GoResults()
        {
            this.Results = new ResultsViewModel();
            await App.Navigator.PushAsync(new ResultsPage());
        }


    }
}
