namespace Voting.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EventsController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly IUserHelper userHelper;

        public EventsController(IEventRepository eventRepository, IUserHelper userHelper)
        {
            this.eventRepository = eventRepository;
            this.userHelper = userHelper;
        }

        public async Task<IActionResult> DeleteCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var candidate = await this.eventRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }
            var eventId = await this.eventRepository.DeleteCandidateAsync(candidate);
            return this.RedirectToAction($"Details/{eventId}");
        }

        public async Task<IActionResult> EditCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var candidate = await this.eventRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }
            return View(candidate);

        }

        [HttpPost]
        public async Task<IActionResult> EditCandidate(CandidateViewModel view)
        {
            if (this.ModelState.IsValid)
            {
                var eventId = await this.eventRepository.UpdateCandidateAsync(view);
                if (eventId != 0)
                {
                    return this.RedirectToAction($"Details/{eventId}");
                }
            }
            return this.View(view);
        }

        public async Task<IActionResult> AddCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = await this.eventRepository.GetByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }
            var model = new CandidateViewModel { EventId = @event.Id };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate(CandidateViewModel view)
        {
            if (this.ModelState.IsValid)
            {
                var path = string.Empty;
                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    path = Path.Combine(
                        Directory.GetCurrentDirectory(), 
                        "wwwroot\\images\\Candidates", 
                        view.ImageFile.FileName);                               
                
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }
                    path = $"~/images/Candidates/{view.ImageFile.FileName}";
                    view.ImageUrl = path;
                }
                // TODO: Pending to change to: this.User.Identity.Name
                await this.eventRepository.AddCandidateAsync(view);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }
        
        // GET: Events
            public IActionResult Index()
        {
            return View(this.eventRepository.GetEventsWithCandidates());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await this.eventRepository.GetEventWithCandidatesAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }







        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                //TODO: Change for the logged user
                @event.User = await this.userHelper.GetUserByEmailAsync("cardonaloaizasebastian112@gmail.com");
                await this.eventRepository.CreateAsync(@event);
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await this.eventRepository.GetByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event @event)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: Change for the logged user
                    @event.User = await this.userHelper.GetUserByEmailAsync("cardonaloaizasebastian112@gmail.com");
                    await this.eventRepository.UpdateAsync(@event);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.eventRepository.ExistAsync(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await this.eventRepository.GetByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await this.eventRepository.GetByIdAsync(id);
            await this.eventRepository.DeleteAsync(@event);
            return RedirectToAction(nameof(Index));
        }

    }
}
