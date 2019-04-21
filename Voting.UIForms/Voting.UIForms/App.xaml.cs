namespace Voting.UIForms
{
    using System;
    using Common.Helpers;
    using Common.Models;
    using Newtonsoft.Json;
    using ViewModels;
    using Views;
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
                var user = JsonConvert.DeserializeObject<User>(Settings.User);
                if (token.Expiration > DateTime.Now)
                {
                    var mainviewModel = MainViewModel.GetInstance();
                    mainviewModel.Token = token;
                    mainviewModel.User = user;
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
