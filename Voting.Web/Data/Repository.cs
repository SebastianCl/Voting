namespace Voting.Web.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Event> GetEvents()
        {
            return this.context.Events.OrderBy(e => e.Name);
        }

        public Event GetEvent(int id)
        {
            return this.context.Events.Find(id);
        }

        public void AddEvent(Event @event)
        {
            this.context.Events.Add(@event);
        }

        public void UpdateEvent(Event @event)
        {
            this.context.Update(@event);
        }

        public void RemoveEvent(Event @event)
        {
            this.context.Events.Remove(@event);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool EventExists(int id)
        {
            return this.context.Events.Any(e => e.Id == id);
        }
    }

}
