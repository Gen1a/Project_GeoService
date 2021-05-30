using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_GeoService.Exceptions;
using Project_GeoService.Interfaces;
using Project_GeoService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_GeoService.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryRepository _repository;

        public CountryController(ICountryRepository repository)
        {
            this._repository = repository;
        }

        // GET: api/country
        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<Country>> GetAll([FromQuery] string continent, [FromQuery] string capital)
        {
            try
            {
                if (!string.IsNullOrEmpty(continent) && !string.IsNullOrEmpty(capital))
                {
                    return _repository.GetAll(continent, capital).ToList();
                }
                else if (!string.IsNullOrEmpty(continent) && string.IsNullOrEmpty(capital))
                {
                    return _repository.GetAllByContinent(continent).ToList();
                }
                else if (string.IsNullOrEmpty(continent) && !string.IsNullOrEmpty(capital))
                {
                    return _repository.GetAllByCapital(capital).ToList();
                }
                else return _repository.GetAll().ToList();
            }
            catch(CountryException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/country/{id}
        [HttpGet("{id}", Name ="Get")]
        [HttpHead("{id}")]
        public ActionResult<Country> Get(long id)
        {
            try
            {
                return _repository.GetCountry(id);
            }
            catch (CountryException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
