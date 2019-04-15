namespace Voting.Web.Controllers.API
{
    using Data.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VotesController : Controller
    {
        private readonly IVoteRepository voteRepository;

        public VotesController(IVoteRepository voteRepository)
        {
            this.voteRepository = voteRepository;
        }

        [HttpGet]
        public IActionResult GetVotes()
        {
            return this.Ok(this.voteRepository.GetVotes());
        }

        [HttpGet("{eventId}")]
        public IActionResult GetVotesOfEvent([FromRoute] int eventId)
        {
            return this.Ok(this.voteRepository.GetVotesOfEvent(eventId));
        }

        /*[HttpGet("{candidateId}")]
        public IActionResult GetVotesOfCandidate([FromRoute] int candidateId)
        {
            return this.Ok(this.voteRepository.GetVotesOfCandidate(candidateId));
        }*/
        

    }
}
