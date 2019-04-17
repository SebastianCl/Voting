namespace Voting.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc;

    public interface IVoteRepository : IGenericRepository<Vote>
    {
        Task<int> AddVoteAsync(Vote Vote);

        IQueryable GetVotesWithAll();

        IQueryable GetVotesOfEvent(int id);

        //Task<IActionResult> PostVote();

        //IQueryable GetVotesOfCandidate(int id);

    }
}
