namespace Voting.Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Services;

    public class EditProfileViewModel : MvxViewModel
    {
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private readonly IDialogService dialogService;

        private List<Country> countries;
        private List<City> cities;
        private Country selectedCountry;
        private City selectedCity;

        private User user;
        private MvxCommand editCommand;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string confirmPassword;
        private string gender;
        private string occupation;
        private int stratum;
        private DateTime birthdate;

        public EditProfileViewModel(
            IMvxNavigationService navigationService,
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.LoadCountries();
        }

        public User User
        {
            get => this.user;
            set => this.SetProperty(ref this.user, value);
        }


        public ICommand EditCommand
        {
            get
            {
                this.editCommand = this.editCommand ?? new MvxCommand(this.EditProfile);
                return this.editCommand;
            }
        }

        private void EditProfile()
        {
            throw new NotImplementedException();
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


        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                RaisePropertyChanged(() => SelectedCity);
            }
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



        private async void LoadCountries()
        {
            var response = await this.apiService.GetListAsync<Country>(
                "https://uvoting.azurewebsites.net",
                "/api",
                "/Countries");

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", "Error al cargar paises", "Aceptar");
                return;
            }
            this.Countries = (List<Country>)response.Result;
            this.SetCountryAndCity();
        }

        private void SetCountryAndCity()
        {
            foreach (var country in this.Countries)
            {
                var city = country.Cities
                    .Where(c => c.Id == this.User.CityId)
                    .FirstOrDefault();

                if (city != null)
                {
                    this.SelectedCountry = country;
                    this.SelectedCity = city;
                    return;
                }
            }
        }





    }
}
