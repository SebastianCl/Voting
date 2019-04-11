namespace Voting.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Models;    

    public interface IEventRepository : IGenericRepository<Event>
    {        
        IQueryable GetEventsWithCandidatesAvailable();

        Task<Event> GetEventWithCandidatesAsync(int id);

        Task<Candidate> GetCandidateAsync(int id);

        Task AddCandidateAsync(CandidateViewModel model);

        Task<int> UpdateCandidateAsync(Candidate candidate);

        Task<int> DeleteCandidateAsync(Candidate candidate);

    }
}
