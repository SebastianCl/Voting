namespace Voting.Web.Data.Repositories
{
    using Entities;
    using System.Threading.Tasks;

    public class ResultRepository : GenericRepository<Result>, IResultRepository
    {
        private readonly DataContext context;

        public ResultRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task GetResults()
        {
            
        }

        }
}
