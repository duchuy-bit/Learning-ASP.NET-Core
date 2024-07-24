namespace ShopManager.Models
{
    public class Category
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
