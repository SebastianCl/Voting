namespace Voting.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;

    [Authorize(Roles = "Admin")]
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


        #region EVENT
        // GET: Events
        public IActionResult Index()
        {
            return View(this.eventRepository.GetEventsWithCandidatesAvailable());
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

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
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
                return new NotFoundViewResult("EventNotFound");
            }

            var @event = await this.eventRepository.GetByIdAsync(id.Value);
            if (@event == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Event @event)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    @event.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await this.eventRepository.UpdateAsync(@event);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.eventRepository.ExistAsync(@event.Id))
                    {
                        return new NotFoundViewResult("EventNotFound");
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
                return new NotFoundViewResult("EventNotFound");
            }

            var @event = await this.eventRepository.GetByIdAsync(id.Value);
            if (@event == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await this.eventRepository.GetEventWithCandidatesAsync(id);
            if (@event.Candidates.Count > 0)
            {
                return RedirectToAction(nameof(NotPermit));
            }
            await this.eventRepository.DeleteAsync(@event);
            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region CANDIDATE
        public async Task<IActionResult> DeleteCandidate(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }
            var candidate = await this.eventRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }
            var eventId = await this.eventRepository.DeleteCandidateAsync(candidate);
            return this.RedirectToAction($"Details/{eventId}");
        }

        public async Task<IActionResult> EditCandidate(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }
            var candidate = await this.eventRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }
            var view = this.ToCandidateViewModel(candidate);
            return View(view);

        }
        [HttpPost]
        public async Task<IActionResult> EditCandidate(CandidateViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = view.ImageUrl;
                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        path = await this.PathImage(view);
                    }
                    var candidate = this.ToCandidate(view, path);
                    await this.eventRepository.UpdateCandidateAsync(candidate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.eventRepository.ExistAsync(view.Id))
                    {
                        return new NotFoundViewResult("EventNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        public async Task<IActionResult> AddCandidate(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }
            var @event = await this.eventRepository.GetByIdAsync(id.Value);
            if (@event == null)
            {
                return new NotFoundViewResult("EventNotFound");
            }
            var model = new CandidateViewModel { EventId = @event.Id };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate(CandidateViewModel view)
        {
            if (this.ModelState.IsValid)
            {
                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    view.ImageUrl = await this.PathImage(view);
                }
                await this.eventRepository.AddCandidateAsync(view);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }
        private CandidateViewModel ToCandidateViewModel(Candidate candidate)
        {
            return new CandidateViewModel
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Proposal = candidate.Proposal,
                ImageUrl = candidate.ImageUrl
            };
        }

        private Candidate ToCandidate(CandidateViewModel candidate, string path)
        {
            return new Candidate
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Proposal = candidate.Proposal,
                ImageUrl = path
            };
        }



        private async Task<string> PathImage(CandidateViewModel view)
        {
            if (view != null)
            {
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\images\\Candidates",
                    file);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await view.ImageFile.CopyToAsync(stream);
                }

                return $"~/images/Candidates/{file}";
            }
            return null;
        }
        #endregion


        #region DEFAULT
        public IActionResult EventNotFound()
        {
            return this.View();
        }

        public IActionResult NotPermit()
        {
            return this.View();
        }
        #endregion


    }
}
