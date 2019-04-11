namespace Voting.Web.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc;

    public interface IVoteRepository : IGenericRepository<Vote>
    {
        Task<int> AddVoteAsync(Vote Vote);

        IQueryable GetVotes();
    }
}
