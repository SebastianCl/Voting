namespace Voting.UIForms.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Views;
    using Voting.Common.Helpers;
    using Voting.Common.Models;
    using Voting.Common.Services;
    using Xamarin.Forms;

    public class LoginViewModel: BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

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


        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsRemember { get; set;}

        public ICommand LoginCommand => new RelayCommand(Login);

        public ICommand RegisterCommand => new RelayCommand(this.Register);

        public ICommand RememberPasswordCommand => new RelayCommand(this.RememberPassword);
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsRemember = true;

            this.Email = "cardonaloaizasebastian112@gmail.com";
            this.Password = "123456";
        }

        private async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        private async void RememberPassword()
        {
            MainViewModel.GetInstance().RememberPassword = new RememberPasswordViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RememberPasswordPage());
        }


        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an password",
                    "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };
            var url = Application.Current.Resources["UrlAPI"].ToString();//como devuelve un objeto tipo object, convierto a string
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email or password incorrect.", "Accept");
                return;
            }
            var token = (TokenResponse)response.Result;//deserealizo el token
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Events = new EventsViewModel();

            Settings.IsRemember = this.IsRemember;
            Settings.UserEmail = this.Email;
            Settings.UserPassword = this.Password;
            Settings.Token = JsonConvert.SerializeObject(token);
            //await Application.Current.MainPage.Navigation.PushAsync(new EventsPage());
            Application.Current.MainPage = new MasterPage();

        }
    }
}
