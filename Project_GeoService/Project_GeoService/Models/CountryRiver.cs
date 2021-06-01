namespace Project_GeoService.Models
{
    public class CountryRiver
    {
        public long CountryId { get; set; }
        public long RiverId { get; set; }
        public Country Country { get; set; }
        public River River { get; set; }
    }
}
