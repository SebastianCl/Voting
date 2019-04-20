using Voting.Common.Models;

namespace Voting.UIForms.ViewModels
{
    public class VoteViewModel
    {
        public Event Event { get; set; }

        public VoteViewModel(Event @event)
        {
            this.Event = @event;
        }
    }
}
