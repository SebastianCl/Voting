namespace Voting.Web.Data.Repositories
{
    using System.Linq;
    using Entities;

    public interface IVoteRepository : IGenericRepository<Vote>
    {
        IQueryable GetEventsWithCandidates();
    }
}
