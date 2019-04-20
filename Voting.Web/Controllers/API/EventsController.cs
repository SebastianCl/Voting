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

        public EventsController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
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
        
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] Common.Models.Event @event)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            
            var entityEvent = new Event
            {
                Name = @event.Name,
                Description = @event.Description,
                StartDate = @event.StartDate,
                FinishDate = @event.FinishDate
            };
            var newEvent = await this.eventRepository.CreateAsync(entityEvent);
            return Ok(newEvent);
        }
        

    }
}
