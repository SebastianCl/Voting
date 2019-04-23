namespace Voting.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public interface IVoteRepository : IGenericRepository<Vote>
    {
        Task<int> AddVoteAsync(Vote Vote);

        IQueryable GetVotesWithAll();

        IQueryable GetVotesOfEvent(int id);

        IQueryable GetVotesOfCandidate(int id);

        IQueryable GetVotesOfUser(string email);

        IQueryable GetVotesOfUserInEvent(string email, int idEvent);

        int GetNumberVotes(string email, int idEvent);

    }
}
