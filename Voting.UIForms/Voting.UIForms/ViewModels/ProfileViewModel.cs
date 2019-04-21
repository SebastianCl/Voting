namespace Voting.UIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Common.Helpers;
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Newtonsoft.Json;
    using Views;
    using Xamarin.Forms;

    public class ProfileViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Country> countries;
        private Country country;
        private ObservableCollection<City> cities;
        private City city;
        private User user;
        private List<Country> myCountries;

        public ICommand SaveCommand => new RelayCommand(this.Save);

        public ICommand ModifyPasswordCommand => new RelayCommand(this.ModifyPassword);


        public Country Country
        {
            get => this.country;
            set
            {
                this.SetValue(ref this.country, value);
                this.Cities = new ObservableCollection<City>(this.Country.Cities.OrderBy(c => c.Name));
            }
        }
        public City City
        {
            get => this.city;
            set => this.SetValue(ref this.city, value);
        }
        public User User
        {
            get => this.user;
            set => this.SetValue(ref this.user, value);
        }
        public ObservableCollection<Country> Countries
        {
            get => this.countries;
            set => this.SetValue(ref this.countries, value);
        }
        public ObservableCollection<City> Cities
        {
            get => this.cities;
            set => this.SetValue(ref this.cities, value);
        }
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

        public ProfileViewModel()
        {
            this.apiService = new ApiService();
            this.User = MainViewModel.GetInstance().User;
            this.IsEnabled = true;
            this.LoadCountries();
        }

        private async void LoadCountries()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Country>(
                url,
                "/api",
                "/Countries");
            this.IsRunning = false;
            this.IsEnabled = true;
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                Languages.Accept);
                return;
            }
            this.myCountries = (List<Country>)response.Result;
            this.Countries = new ObservableCollection<Country>(myCountries);
            this.SetCountryAndCity();
        }
        private void SetCountryAndCity()
        {
            foreach (var country in this.myCountries)
            {
                var city = country.Cities.Where(c => c.Id == this.User.CityId).FirstOrDefault();
                if (city != null)
                {
                    this.Country = country;
                    this.City = city;
                    return;
                }
            }
        }


        private async void Save()
        {
            if (string.IsNullOrEmpty(this.User.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.FirstNameError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LastNameError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailError,
                    Languages.Accept);
                return;
            }

            if (!RegexHelper.IsValidEmail(this.User.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailFormatError,
                    Languages.Accept);
                return;
            }

            if (this.Country == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CountryError,
                    Languages.Accept);
                return;
            }

            if (this.City == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.CityError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.Occupation))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.OccupationError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.Gender))
            {
                ;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.GenderError,
                    Languages.Accept);
                return;
            }

            if (this.User.Stratum <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.StratumError,
                    Languages.Accept);
                return;
            }

            if (this.User.Birthdate > DateTime.Now)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.BirthdateError,
                    Languages.Accept);
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;
            var url = Application.Current.Resources["UrlAPI"].ToString();

            var response = await this.apiService.PutAsync(
                url,
                "/api",
                "/Account",
                this.User,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            MainViewModel.GetInstance().User = this.User;
            Settings.User = JsonConvert.SerializeObject(this.User);

            await Application.Current.MainPage.DisplayAlert(
                Languages.Ok,
                Languages.UserUpdated,
                Languages.Accept);
            await App.Navigator.PopAsync();
        }

        private async void ModifyPassword()
        {
            MainViewModel.GetInstance().ChangePassword = new ChangePasswordViewModel();
            await App.Navigator.PushAsync(new ChangePasswordPage());
        }

    }
}
