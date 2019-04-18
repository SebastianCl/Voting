namespace Voting.Web.Controllers.API
{
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
        private readonly IUserHelper userHelper;
        private readonly IEventRepository eventRepository;

        public VotesController(
            IVoteRepository voteRepository,
            IUserHelper userHelper,
            IEventRepository eventRepository)
        {
            this.voteRepository = voteRepository;
            this.userHelper = userHelper;
            this.eventRepository = eventRepository;
        }

        [HttpGet]
        public IActionResult GetVotes()
        {
            return this.Ok(this.voteRepository.GetVotesWithAll());
        }

        //[Route("Event/{eventId}")]
        [HttpGet("/{eventId}")]
        public IActionResult GetVotesOfEvent([FromRoute] int eventId)
        {
            return this.Ok(this.voteRepository.GetVotesOfEvent(eventId));
        }

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