namespace Voting.Web.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        private readonly DataContext context;

        public VoteRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<int> AddVoteAsync(Vote Vote)
        {
            var @event = await this.context.Events.Where(c => c.Candidates.Any(ca => ca.Id == Vote.Id)).FirstOrDefaultAsync();
            if (@event == null)
            {
                return 0;
            }
            this.context.Votes.Update(Vote);
            await this.context.SaveChangesAsync();
            return @event.Id;
        }

        

        public IQueryable GetEventsWithCandidates()
        {
            return this.context.Events
            .Include(c => c.Candidates)
            .Include(u => u.User)
            .OrderBy(e => e.FinishDate);
        }

        public IQueryable GetEventWithCandidatesAndVotes()
        {
            return this.context.Votes
                .Include(c => c.Candidate);
        }

        public async Task<Event> GetEventWithCandidatesAsync(int id)
        {
            return await this.context.Events
            .Include(c => c.Candidates)
            .Where(c => c.Id == id)
            .Include(u => u.User)
            .FirstOrDefaultAsync();
        }
               
    }
}
