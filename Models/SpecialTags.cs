using System.ComponentModel.DataAnnotations;

namespace B2BApp.Models
{
    public class SpecialTags
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tag Name")]
        public string SpecialTag { get; set; }


    }
}
