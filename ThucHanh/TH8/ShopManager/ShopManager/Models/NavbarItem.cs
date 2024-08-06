namespace ShopManager.Models
{
    public class NavbarItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? MenuUrl { get; set; }
        public int MenuIndex { get; set; }
        public bool isVisible { get; set; }

        public List<NavbarItem>? subItems { get; set; }
    }
}
