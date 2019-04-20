namespace Voting.Web.Controllers
{
    using System.Threading.Tasks;
    using Data;
    using Data.Repositories;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Voting.Web.Data.Entities;

    [Authorize(Roles = "Admin")]
    public class VotesController : Controller
    {
        private readonly IVoteRepository votetRepository;
        private readonly IEventRepository eventRepository;
        private readonly IUserHelper userHelper;

        public VotesController(IVoteRepository votetRepository, IEventRepository eventRepository, IUserHelper userHelper)
        {
            this.votetRepository = votetRepository;
            this.eventRepository = eventRepository;
            this.userHelper = userHelper;
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

            return View(@event);
        }

        [HttpPost]
        public async Task<IActionResult> AddVote(Vote view)
        {
            if (this.ModelState.IsValid)
            {
                var vote = new Vote {
                    Candidate= view.Candidate,
                    Event = view.Event,
                    User = view.User
                        
                } ;
                await this.votetRepository.AddVoteAsync(vote);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }


    }
}