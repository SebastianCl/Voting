namespace Voting.UIForms
{
    using Newtonsoft.Json;
    using System;
    using Views;
    using Voting.Common.Helpers;
    using Voting.Common.Models;
    using Voting.UIForms.ViewModels;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();
            if (Settings.IsRemember)
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                if (token.Expiration > DateTime.Now)
                {
                    var mainviewModel = MainViewModel.GetInstance();
                    mainviewModel.Token = token;
                    mainviewModel.UserEmail = Settings.UserEmail;
                    mainviewModel.UserPassword = Settings.UserPassword;
                    mainviewModel.Events = new EventsViewModel();
                    this.MainPage = new MasterPage();
                    return;
                }
            }
            //Antes de instanciar una Page, instanciamos la viewModel asociada
            MainViewModel.GetInstance().Login = new LoginViewModel();
            this.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
