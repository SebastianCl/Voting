﻿namespace Voting.Common.ViewModels
{
    using Services;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;

    public class EventsDetailViewModel : MvxViewModel<NavigationArgs>
    {
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private Event @event;
        private bool isLoading;
        private MvxCommand<Candidate> candidateClickCommand;

        public EventsDetailViewModel(
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.IsLoading = false;
        }

        public ICommand CandidateClickCommand
        {
            get
            {
                this.candidateClickCommand = new MvxCommand<Candidate>(this.OnCandidateClickCommand);
                return candidateClickCommand;
            }
        }


        private void OnCandidateClickCommand(Candidate candidate)
        {
            this.dialogService.Confirm(
                "Confirm vote",
                $"You are sure to vote for '{candidate.Name}' ",
                "Yes",
                "No",
                () => { this.VoteToCandidate(candidate); },
                null);
        }

        private async void VoteToCandidate(Candidate candidate)
        {
            var request = new NewVote
            {
                Email = Settings.UserEmail,
                Candidate = candidate.Id,
                Event = Event.Id
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.RegisterVoteAsync(
                "https://uvoting.azurewebsites.net",
                "/api",
                "/Votes",
                request,
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert(
                    "Information",
                    response.Message,
                    "Accept");
                return;
            }

            this.dialogService.Alert(
                "Ok",
                "Your vote for " + candidate.Name + " was saved",
                "Accept");
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public Event Event
        {
            get => this.@event;
            set => this.SetProperty(ref this.@event, value);
        }


        public override void Prepare(NavigationArgs parameter)
        {
            this.@event = parameter.Event;
        }

    }

}
