namespace Voting.Web.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc;
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


        #region API
        public IQueryable GetVotes()
        {
            return this.context.Votes
                .Include(e => e.Event)
                .Include(u => u.User)
                .Include(c => c.Candidate);
        }

        public IQueryable GetVotesOfEvent(int id)
        {
            return this.context.Votes
                .Include(e => e.Event)
                .Include(u => u.User)
                .Include(c => c.Candidate)
                .Where(e => e.Event.Id == id);

        }
        #endregion

    }
}
