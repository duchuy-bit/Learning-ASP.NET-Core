using System.ComponentModel.DataAnnotations;

namespace ShopManager.Models
{
    public class CategoryMenu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Count { get; set; }

    }
}
