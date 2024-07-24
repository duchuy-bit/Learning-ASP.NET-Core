using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShopManager.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Title")]
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "*")]
        [MaxLength(500, ErrorMessage = "Tối đa 500 kí tự")]
        public string Content { get; set; }

        public string Img { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "*")]
        public int Price { get; set; }

        [Display(Name = "Discount")]
        [Required(ErrorMessage = "*")]
        public double Discount { get; set; }

        [Display(Name = "Create At")]
        public DateTime CreateAt { get; set; }

        [Display(Name = "Update At")]
        public DateTime UpdateAt { get; set; }

        [Display(Name = "Category Id")]
        [Required(ErrorMessage = "*")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Title")]
        [Required(ErrorMessage = "*")]
        public string CategoryTitle { get; set; }
    }

    public class ProductForm : Product
    {
        //[Display(Name = "Category")]
        //[Required(ErrorMessage = "*")]
        public string IdCategorySelected { get; set; }
        public List<SelectListItem>? ListCategory { get; set; }

    }
}
