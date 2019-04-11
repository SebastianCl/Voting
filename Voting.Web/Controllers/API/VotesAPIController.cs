namespace Voting.Web.Controllers.API
{
    using Data.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class VotesApiController : Controller
    {
        private readonly IVoteRepository voteRepository;

        public VotesApiController(IVoteRepository voteRepository)
        {
            this.voteRepository = voteRepository;
        }

        [HttpGet]
        public IActionResult GetVotes()
        {
            return Ok(this.voteRepository.GetVotes());
        }
    }
}