namespace Voting.Web.Controllers.API
{
    using Data;
    using Data.Repositories;
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





    }
}
