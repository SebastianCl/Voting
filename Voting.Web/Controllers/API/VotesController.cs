namespace Voting.Web.Controllers.API
{
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
        private readonly IUserHelper userHelper;

        public VotesController(
            IVoteRepository voteRepository,
            IUserHelper userHelper)
        {
            this.voteRepository = voteRepository;
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

        /*[HttpPost]
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
            //TODO: Upload images
            var entityEvent = new Vote
            {
                Event = vote.ev
                LastPurchase = vote.LastPurchase,
                LastSale = vote.LastSale,
                Name = vote.Name,
                Price = vote.Price,
                Stock = vote.Stock,
                User = user
            };
            var newProduct = await this.eventRepository.CreateAsync(entityVote);
            return Ok(newProduct);
        }*/




    }
}