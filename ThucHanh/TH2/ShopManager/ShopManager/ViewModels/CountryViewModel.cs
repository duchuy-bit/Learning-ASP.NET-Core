using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopManager.ViewModels
{
    public class CountryViewModel
    {

        public string SelectedCountry { get; set; }
        public List<SelectListItem> myListCountry {  get; set; }    
    }
}
