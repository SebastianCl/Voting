namespace Voting.Web.Controllers.API
{
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventsController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly IUserHelper userHelper;

        public EventsController(
            IEventRepository eventRepository,
            IUserHelper userHelper)
        {
            this.eventRepository = eventRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            return this.Ok(this.eventRepository.GetEventWithCandidates());
        }

        [HttpGet("{id}")]
        public IActionResult GetEventWithId([FromRoute] int id)
        {
            return this.Ok(this.eventRepository.GetEventWithId(id));
        }
        /*
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] Common.Models.Event @event)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(@event.User.Email);
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            var entityEvent = new Event
            {
                Name = @event.Name,
                Description = @event.Description,
                StartDate = @event.StartDate,
                FinishDate = @event.FinishDate,
                User = user
            };
            var newEvent = await this.eventRepository.CreateAsync(entityEvent);
            return Ok(newEvent);
        }
        */

    }
}
