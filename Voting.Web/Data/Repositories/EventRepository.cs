namespace Voting.Web.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly DataContext context;

        public EventRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        #region PAGE
        public async Task AddCandidateAsync(CandidateViewModel model)
        {
            var @event = await this.GetEventWithCandidatesAsync(model.Id);
            if (@event == null)
            {
                return;
            }
            @event.Candidates.Add(new Candidate
            {
                Name = model.Name,
                Proposal = model.Proposal,
                ImageUrl = model.ImageUrl
            });
            this.context.Events.Update(@event);
            await this.context.SaveChangesAsync();
        }

        public async Task<int> UpdateCandidateAsync(Candidate candidate)
        {
            var @event = await this.context.Events.Where(c => c.Candidates.Any(ca => ca.Id == candidate.Id)).FirstOrDefaultAsync();
            if (@event == null)
            {
                return 0;
            }
            this.context.Candidates.Update(candidate);
            await this.context.SaveChangesAsync();
            return @event.Id;
        }

        public async Task<int> DeleteCandidateAsync(Candidate candidate)
        {
            var @event = await this.context.Events.Where(c => c.Candidates.Any(ca => ca.Id == candidate.Id)).FirstOrDefaultAsync();
            if (@event == null)
            {
                return 0;
            }
            this.context.Candidates.Remove(candidate);
            await this.context.SaveChangesAsync();
            return @event.Id;
        }

        public Task<Candidate> GetCandidateAsync(int id)
        {
            return this.context.Candidates.FindAsync(id);
        }

        public Task<Event> GetEventWithCandidatesAsync(int id)
        {
            return this.context.Events
            .Include(c => c.Candidates)
            .Where(c => c.Id == id)
            .Include(u => u.User)
            .FirstOrDefaultAsync();
        }


        public IQueryable GetEventsWithCandidatesAvailable()
        {
            return this.context.Events
            .Include(c => c.Candidates)
            .Include(u => u.User)
            .Where(d => d.FinishDate > DateTime.Now)
            .OrderBy(e => e.FinishDate);
        }


        #endregion

        #region API
        public IQueryable GetEventWithCandidates()
        {
            return this.context.Events
                .Include(c => c.Candidates);
        }

        public IQueryable GetEventWithId(int id)
        {
            return this.context.Events
            .Include(c => c.Candidates)
            .Where(c => c.Id == id);
        }
        #endregion





    }
}
