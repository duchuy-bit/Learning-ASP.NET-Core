using System.ComponentModel.DataAnnotations;

namespace ShopManager.Areas.Admin.Models
{
    public class MenuAdmin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string Title { get; set; }
        public int? ParentId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string? MenuUrl { get; set; }
        public int MenuIndex { get; set; }
        public bool IsVisible { get; set; }

        public List<MenuAdmin>? SubItems { get; set; }
    }

    public class MenuAdmin_Parent_SubMenu
    {
        public MenuAdmin MenuParent { get; set; }
        public MenuAdmin SubMenu { get; set; }

    }
}
