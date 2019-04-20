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

        IQueryable GetVotesOfCandidate(int id);

        IQueryable GetVotesOfUser(string idUser, int idEvent);

        //Task<IActionResult> PostVote();

        //IQueryable GetVotesOfCandidate(int id);

    }
}
