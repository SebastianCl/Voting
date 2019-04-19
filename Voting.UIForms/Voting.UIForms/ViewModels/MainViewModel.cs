using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Voting.Common.Models;

namespace Voting.UIForms.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;//singleton(apuntador a la misma clase)
        public LoginViewModel Login { get; set; }

        public EventsViewModel Events { get; set; }

        public TokenResponse Token { get; set; }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }        public RegisterViewModel Register { get; set; }        

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
                    PageName = "SetupPage",
                    Title = "Setup"
                },

                new Menu
                {
                    Icon = "ic_info_outline",
                    PageName = "AboutPage",
                    Title = "About"
                },

                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Close session"
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
    }
}
