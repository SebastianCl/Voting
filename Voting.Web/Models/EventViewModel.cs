namespace Voting.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;    
    using Data.Entities;

    public class EventViewModel : Event
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
