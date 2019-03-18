namespace Voting.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Candidate : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Display(Name = "Option Name")]
        [Required]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [Required]
        public string Proposal { get; set; }

        [Display(Name = "Image")]
        [Required]
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
