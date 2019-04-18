﻿namespace Voting.Web.Controllers.API
{
    using Data;
    using Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Voting.Web.Data.Entities;

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

        /*[HttpGet("{eventId}")]
        public IActionResult GetVotesOfEvent([FromRoute] int eventId)
        {
            return this.Ok(this.voteRepository.GetVotesOfEvent(eventId));
        }*/

        /*[HttpGet("{candidateId}")]
        public IActionResult GetVotesOfCandidate([FromRoute] int candidateId)
        {
            return this.Ok(this.voteRepository.GetVotesOfCandidate(candidateId));
        }*/

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

            var candidate = await this.eventRepository.GetCandidateAsync(vote.Candidate.Id);
            if (candidate == null)
            {
                return this.BadRequest("Invalid candidate");
            }

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