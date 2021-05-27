using System;

namespace Project_GeoService.Exceptions
{
    public class CountryException : Exception
    {
        public CountryException(string message) : base(message) { }
    }
}
