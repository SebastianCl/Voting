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
        public IQueryable GetVotesWithAll()
        {
            return this.context.Votes
                .Include(u => u.User)                
                .Include(e => e.Event)
                .Include(c => c.Candidate);
        }

        public IQueryable GetVotesOfEvent(int id)
        {
            var res = this.context.Votes
                .Include(e => e.Event)
                .Include(u => u.User)
                .Include(c => c.Candidate)
                .Where(e => e.Event.Id == id);
            return res;
        }

        public IQueryable GetVotesOfCandidate(int id)
        {
            return this.context.Votes
                .Include(e => e.Event)
                .Include(c  => c.Candidate)
                .Where(c => c.Candidate.Id == id);
        }

        public IQueryable GetVotesOfUser(string idUser, int idEvent)
        {
            var res = this.context.Votes
                .Include(e => e.Event)
                .Include(u => u.User)
                .Where(e => e.Event.Id == idEvent)
                .Where(u => u.User.Id == idUser);
            return res;
        }
        
        #endregion

    }
}
