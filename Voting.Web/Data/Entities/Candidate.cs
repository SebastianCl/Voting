namespace Voting.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Candidate : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Candidate")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]               
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters length.")]        
        public string Proposal { get; set; }
                
        [Display(Name = "Image")]        
        public string ImageUrl { get; set; }
        
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageUrl))
                {
                    return null;
                }

                return $"https://voting.azurewebsites.net{this.ImageUrl.Substring(1)}";
            }
        }        
    }
}
