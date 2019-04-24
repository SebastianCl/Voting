namespace Voting.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Voting.Web.Data.Entities;

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

            string winner = this.GetWinner(@event.Candidates);

            var result = new ResultViewModel
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Candidates = @event.Candidates,
                StartDate = @event.StartDate,
                FinishDate = @event.FinishDate,
                Winner = winner

            };

            return View(result);
        }


        private string GetWinner(ICollection<Candidate> candidates)
        {
            var  myWinner = candidates;
            int maxVote = 0;
            var winner = "";

            foreach (var item in myWinner)
            {
                if (item.TotalVotes > maxVote)
                {
                    winner = "";
                    maxVote = item.TotalVotes;
                    winner = item.Name;
                }
                else if (item.TotalVotes == maxVote)
                {
                    winner = $"{winner}, {item.Name}";
                }
            }
            winner = $"{winner} with {maxVote} votes";
            return winner;

        }

    }
}