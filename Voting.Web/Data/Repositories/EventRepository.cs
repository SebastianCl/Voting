namespace Voting.Web.Data
{
    using Entities;
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(DataContext context) : base(context)
        {
        }
    }
}
