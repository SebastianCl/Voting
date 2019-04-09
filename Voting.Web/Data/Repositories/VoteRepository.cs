namespace Voting.Web.Data.Repositories
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        private readonly DataContext context;

        public VoteRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetEventsWithCandidates()
        {
            return this.context.Events
            .Include(c => c.Candidates)
            .Include(u => u.User)
            .OrderBy(e => e.FinishDate);
        }
    }
}
