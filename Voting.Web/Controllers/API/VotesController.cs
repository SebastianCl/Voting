namespace Voting.Web.Controllers.API
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VotesController : Controller
    {
        private readonly IVoteRepository voteRepository;
        private readonly IEventRepository eventRepository;
        private readonly IUserHelper userHelper;

        public VotesController(
            IVoteRepository voteRepository,
            IEventRepository eventRepository,
            IUserHelper userHelper)
        {
            this.voteRepository = voteRepository;
            this.eventRepository = eventRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetVotes()
        {
            return this.Ok(this.voteRepository.GetVotesWithAll());
        }

        [HttpGet("Event/{eventId}")]
        public IActionResult GetVotesOfEvent([FromRoute] int eventId)
        {
            return this.Ok(this.voteRepository.GetVotesOfEvent(eventId));
        }

        [HttpGet("Candidate/{candidateId}")]
        public IActionResult GetVotesOfCandidate([FromRoute] int candidateId)
        {
            return this.Ok(this.voteRepository.GetVotesOfCandidate(candidateId));
        }

        [HttpGet("User")]
        public IActionResult GetVoteOfUser([FromBody] Common.Models.VoteSearch voteSearch)
        {
            return this.Ok(this.voteRepository.GetVotesOfUser(voteSearch.Email));
        }

        [HttpGet("User/Event")]
        public IActionResult GetVoteOfUserInEvent([FromBody] Common.Models.VoteSearch voteSearch)
        {
            return this.Ok(this.voteRepository.GetVotesOfUserInEvent(voteSearch.Email, voteSearch.Event));
        }

        
        [HttpPost]
        public async Task<IActionResult> PostVote([FromBody] Common.Models.Vote vote)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(vote.User.Email);
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            var @event = await this.eventRepository.GetByIdAsync(vote.Event.Id);

            if (@event == null)
            {
                return this.BadRequest("Invalid event");
            }

            if (@event.StartDate > DateTime.Now)
            {
                return this.BadRequest("The voting event has not started yet");
            }

            if (@event.FinishDate < DateTime.Now)
            {
                return this.BadRequest("The voting event has closed");
            }

            /*var userVote = this.voteRepository.GetVotesOfUserInEvent(user.Id, @event.Id);
            if (!userVote.Any())
            {
                return this.BadRequest("You already voted in this event");
            }*/


            var candidate = await this.eventRepository.GetCandidateAsync(vote.Candidate.Id);
            if (candidate == null)
            {
                return this.BadRequest("Invalid candidate");
            }

            candidate.TotalVotes++;
            await this.eventRepository.UpdateCandidateAsync(candidate);

            var entityVote = new Vote
            {
                User = user,
                Event = @event,
                Candidate = candidate
            };
            var newVote = await this.voteRepository.CreateAsync(entityVote);
            return Ok(newVote);
        }




       


    }
}