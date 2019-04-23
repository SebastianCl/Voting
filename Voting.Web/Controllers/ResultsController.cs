namespace Voting.Web.Controllers
{
    using System.Threading.Tasks;
    using Data;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Voting.Web.Models;

    public class ResultsController : Controller
    {
        private readonly IEventRepository eventRepository;

        public ResultsController(IEventRepository eventRepository, IUserHelper userHelper)
        {
            this.eventRepository = eventRepository;
        }

        public IActionResult Index()
        {
            return View(this.eventRepository.GetEventsWithCandidatesFinished());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }

            var @event = await this.eventRepository.GetEventWithCandidatesAsync(id.Value);
            if (@event == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }

            var result = new ResultViewModel
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Candidates = @event.Candidates,
                StartDate = @event.StartDate,
                FinishDate = @event.FinishDate,
                Winner = "Winner"

            };

            return View(result);
        }

        


    }
}