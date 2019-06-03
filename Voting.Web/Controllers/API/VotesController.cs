namespace Voting.Web.Controllers.API
{
    using System;
    using System.Threading.Tasks;
    using Common.Models;
    using Data;
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

        #region USER
        [HttpPost("User")]
        public IActionResult GetVoteOfUser([FromBody] Common.Models.NewVote voteSearch)
        {
            return this.Ok(this.voteRepository.GetVotesOfUser(voteSearch.Email));
        }

        [HttpGet("User/Event")]
        public IActionResult GetVoteOfUserInEvent([FromBody] Common.Models.NewVote voteSearch)
        {
            return this.Ok(this.voteRepository.GetVotesOfUserInEvent(voteSearch.Email, voteSearch.Event));
        }


        [HttpGet("ValidateVote")]
        public IActionResult GetVoteCount([FromBody] Common.Models.NewVote voteSearch)
        {
            return this.Ok(this.voteRepository.GetNumberVotes(voteSearch.Email, voteSearch.Event));
        }
        #endregion



        [HttpPost]
        public async Task<IActionResult> PostVote([FromBody] Common.Models.NewVote vote)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(vote.Email);
            if (user == null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Invalid user"
                });
            }

            var @event = await this.eventRepository.GetByIdAsync(vote.Event);

            if (@event == null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Invalid event"
                });
            }

            if (@event.StartDate > DateTime.Now)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "The voting event has not started yet"
                });
            }

            if (@event.FinishDate < DateTime.Now)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "The voting event has closed"
                });
            }

            int userVote = this.voteRepository.GetNumberVotes(user.Email, @event.Id);
            if (userVote > 0)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "You already voted in this event"
                });
            }

            var candidate = await this.eventRepository.GetCandidateAsync(vote.Candidate);
            if (candidate == null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Invalid candidate"
                });
            }

            candidate.TotalVotes++;
            await this.eventRepository.UpdateCandidateAsync(candidate);

            var entityVote = new Data.Entities.Vote
            {
                User = user,
                Event = @event,
                Candidate = candidate
            };

            await this.voteRepository.CreateAsync(entityVote);
            return Ok(new Response
            {
                IsSuccess = true,
                Message = $"Registered vote. The candidate \"{entityVote.Candidate.Name}\" has {candidate.TotalVotes} votes"
            });
        }


    }
}