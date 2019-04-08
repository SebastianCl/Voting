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

        public EventsController(IEventRepository eventRepository, ICountryRepository countryRepository)
        {
            this.eventRepository = eventRepository;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            return this.Ok(this.eventRepository.GetEventsWithCandidates());
        }

    }
}
