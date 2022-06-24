using System.ComponentModel.DataAnnotations;

namespace B2BApp.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }
    

    }
}
