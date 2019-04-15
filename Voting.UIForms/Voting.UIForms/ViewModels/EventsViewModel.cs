﻿namespace Voting.UIForms.ViewModels
{
    using Common.Models;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EventsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<Event> events;
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }

        public ICommand RefreshCommand => new RelayCommand(this.LoadEvents);

        public ObservableCollection<Event> Events
        {
            get => this.events;
            set => this.SetValue(ref this.events, value);
        }

        public EventsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadEvents();
        }

        private async void LoadEvents()
        {
            this.IsRefreshing = true;
            var response = await this.apiService.GetListAsync<Event>(
                "https://uvoting.azurewebsites.net/",
                "/api",
                "/Events");            

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                this.IsRefreshing = false;
                return;
            }

            var myEvents = (List<Event>)response.Result;
            this.Events = new ObservableCollection<Event>(myEvents);
            this.IsRefreshing = false;
        }
    }
}
