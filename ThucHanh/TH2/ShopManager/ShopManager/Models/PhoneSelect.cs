using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopManager.Models
{
    public class PhoneSelect
    {
        public string phoneSelected { get; set; }
        public List<SelectListItem> listPhone { get; set; }
    }
}
