namespace Voting.Web.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public interface IVoteRepository : IGenericRepository<Vote>
    {
        IQueryable GetEventsWithCandidates();

        Task<int> AddVoteAsync(Vote Vote);
    }
}
