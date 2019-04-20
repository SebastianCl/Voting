namespace Voting.Web.Controllers
{
    using Data;
    using Data.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Voting.Web.Data.Entities;
    using Voting.Web.Models;

    public class ResultsController : Controller
    {
        private readonly IResultRepository resultRepository;
        private readonly IEventRepository eventRepository;
        private readonly IVoteRepository voteRepository;

        public ResultsController(IResultRepository resultRepository, IEventRepository eventRepository, IVoteRepository voteRepository)
        {
            this.resultRepository = resultRepository;
            this.eventRepository = eventRepository;
            this.voteRepository = voteRepository;
        }

        public IActionResult Index()
        {
            var myEvents = this.eventRepository.GetEventsWithCandidatesAvailable();
            var myVotes = this.voteRepository.GetAll();

            return View(myEvents);
        }
    }
}
