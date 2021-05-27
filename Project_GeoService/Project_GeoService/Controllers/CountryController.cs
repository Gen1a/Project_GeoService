using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_GeoService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_GeoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryRepository _repository;

        public CountryController(ICountryRepository repository)
        {
            this._repository = repository;
        }
    }
}
