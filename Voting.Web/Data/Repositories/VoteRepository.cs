﻿namespace Voting.Web.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        private readonly DataContext context;

        public VoteRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<int> AddVoteAsync(Vote Vote)
        {
            var @event = await this.context.Events
                .Where(c => c.Candidates.Any(ca => ca.Id == Vote.Id))
                .FirstOrDefaultAsync();
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
            return this.context.Votes
                .Include(e => e.Event)
                .Include(u => u.User)
                .Include(c => c.Candidate)
                .Where(e => e.Event.Id == id);
        }

        public IQueryable GetVotesOfCandidate(int id)
        {
            return this.context.Votes
                .Include(e => e.Event)
                .Include(c => c.Candidate)
                .Include(u => u.User)
                .Where(c => c.Candidate.Id == id);
        }

        public IQueryable GetVotesOfUser(string email)
        {
            return this.context.Votes
                .Include(e => e.Event)
                .Include(c => c.Candidate)
                .Where(v => v.User.Email == email);

        }

        public IQueryable GetVotesOfUserInEvent(string email, int idEvent)
        {
            return this.context.Votes
                .Include(e => e.Event)
                .Include(c => c.Candidate)
                .Where(e => e.Event.Id == idEvent)
                .Where(v => v.User.Email == email);


        }
        public int GetNumberVotes(string idEmail, int idEvent)
        {
            int Count = this.context.Votes.Count(x => x.User.Email == idEmail && x.Event.Id == idEvent);
            return Count;
        }

        #endregion

    }
}
