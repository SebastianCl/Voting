namespace Voting.Web.Controllers.API
{
    using Data;
    using Data.Repositories;
    using Microsoft.AspNetCore.Mvc;


    [Route("api/[Controller]")]
    public class EventsController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly ICountryRepository countryRepository;

        public EventsController(IEventRepository eventRepository, ICountryRepository countryRepository)
        {
            this.eventRepository = eventRepository;
            this.countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            return this.Ok(this.eventRepository.GetAll());
        }

    }
}
