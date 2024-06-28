namespace ShopManager.ViewModels
{
    public class CountryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class CountryRadio 
    { 
        public string CountryName {  get; set; }

        public List<CountryModel> ListCountry { get; set; }
    }

}
