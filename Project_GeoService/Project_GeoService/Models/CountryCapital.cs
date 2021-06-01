namespace Project_GeoService.Models
{
    public class CountryCapital
    {
        public long CountryId { get; set; }
        public long CityId { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
    }
}
