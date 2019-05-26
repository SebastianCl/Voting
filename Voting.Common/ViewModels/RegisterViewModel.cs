namespace Voting.Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Services;
    using Voting.Common.Helpers;

    public class RegisterViewModel : MvxViewModel
    {
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private readonly IDialogService dialogService;
        private List<Country> countries;
        private List<City> cities;
        private Country selectedCountry;
        private City selectedCity;
        private MvxCommand registerCommand;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string confirmPassword;
        private string gender;
        private string occupation;
        private int stratum;
        private DateTime birthdate;

        public RegisterViewModel(
            IMvxNavigationService navigationService,
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.LoadCountries();
        }

        public ICommand RegisterCommand
        {
            get
            {
                this.registerCommand = this.registerCommand ?? new MvxCommand(this.RegisterUser);
                return this.registerCommand;
            }
        }

        public string FirstName
        {
            get => this.firstName;
            set => this.SetProperty(ref this.firstName, value);
        }

        public string LastName
        {
            get => this.lastName;
            set => this.SetProperty(ref this.lastName, value);
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        public string ConfirmPassword
        {
            get => this.confirmPassword;
            set => this.SetProperty(ref this.confirmPassword, value);
        }

        public string Gender
        {
            get => this.gender;
            set => this.SetProperty(ref this.gender, value);
        }

        public string Occupation
        {
            get => this.occupation;
            set => this.SetProperty(ref this.occupation, value);
        }

        public int Stratum
        {
            get => this.stratum;
            set => this.SetProperty(ref this.stratum, value);
        }

        public DateTime Birthdate
        {
            get => this.birthdate;
            set => this.SetProperty(ref this.birthdate, value);
        }

        public List<Country> Countries
        {
            get => this.countries;
            set => this.SetProperty(ref this.countries, value);
        }

        public List<City> Cities
        {
            get => this.cities;
            set => this.SetProperty(ref this.cities, value);
        }

        public Country SelectedCountry
        {
            get => selectedCountry;
            set
            {
                this.selectedCountry = value;
                this.RaisePropertyChanged(() => SelectedCountry);
                this.Cities = SelectedCountry.Cities;
            }
        }

        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                RaisePropertyChanged(() => SelectedCity);
            }
        }

        private async void LoadCountries()
        {
            var response = await this.apiService.GetListAsync<Country>(
                "https://uvoting.azurewebsites.net",
                "/api",
                "/Countries");

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Countries = (List<Country>)response.Result;
        }

        private async void RegisterUser()
        {
            if (string.IsNullOrEmpty(this.FirstName))
            {
                this.dialogService.Alert("Error", "Debe ingresar el nombre", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                this.dialogService.Alert("Error", "Debe ingresar el apellido", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                this.dialogService.Alert("Error", "Debe ingresar el email", "Aceptar");
                return;
            }

            if (!RegexHelper.IsValidEmail(this.Email))
            {
                this.dialogService.Alert("Error", "Debe ingresar un email valido", "Aceptar");
                return;
            }

            if (this.Countries == null)
            {
                this.dialogService.Alert("Error", "Debe ingresar el pais", "Aceptar");
                return;
            }
            
            if (this.Cities == null)
            {
               this.dialogService.Alert("Error", "Debe ingresar la ciudad", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Occupation))
            {
                this.dialogService.Alert("Error", "Debe ingresar la ocupación", "Aceptar");
                return;
            }

            if (this.Stratum <= 0)
            {
                this.dialogService.Alert("Error", "Debe ingresar un estrato valido", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Gender))
            {
                this.dialogService.Alert("Error", "Debe ingresar el genero", "Aceptar");
                return;
            }

            if (this.Birthdate > DateTime.Now || this.Birthdate == null)
            {
                this.dialogService.Alert("Error", "Debe ingresar la fecha de nacimiento", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.dialogService.Alert("Error", "Debe ingresar una contraseña", "Aceptar");
                return;
            }

            if (this.Password.Length < 6)
            {
                this.dialogService.Alert("Error", "La contraseña debe tener almenos 6 caracteres", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.ConfirmPassword))
            {
                this.dialogService.Alert("Error", "Debe verificar la contraseña", "Aceptar");
                return;
            }

            if (!this.Password.Equals(this.ConfirmPassword))
            {
                this.dialogService.Alert("Error", "Las contraseñas no coinciden ", "Aceptar");
                return;
            }

            var request = new NewUserRequest
            {   
                FirstName = this.FirstName,
                LastName = this.LastName,
                CityId = this.SelectedCity.Id,
                Email = this.Email,
                Password = this.Password,
                Gender = this.Gender,
                Occupation = this.Occupation,
                Stratum = this.Stratum,
                Birthdate = this.Birthdate
            };


            var response = await this.apiService.RegisterUserAsync(
                "https://uvoting.azurewebsites.net",
                "/api",
                "/Account",
                request);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            
            this.dialogService.Alert("Ok", "The user was created succesfully, you must " +
                "confirm your user by the email sent to you and then you could login with " +
                "the email and password entered.", "Accept");

            await this.navigationService.Close(this);
        }
    }
}
