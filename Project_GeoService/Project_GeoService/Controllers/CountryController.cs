using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Project_GeoService.Exceptions;
using Project_GeoService.Interfaces;
using Project_GeoService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // GET: api/countries
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
            catch (CountryException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/countries/{id}
        [HttpGet("{id}", Name = "Get")]
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

        // POST
        [HttpPost]
        public ActionResult<Country> Post([FromBody] Country country)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _repository.AddCountry(country);
            return CreatedAtAction(nameof(Get), new { Id = country.Id }, country);
        }

        // PUT: Tries to execute a FULL update of a resource
        [HttpPut("{id}")]
        public ActionResult Put(long id, [FromBody] Country country)
        {
            if (country == null || id != country.Id) return BadRequest();

            try
            {
                _repository.UpdateCountry(country);
                return new NoContentResult();
            }
            catch (CountryException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Adding new country to database...");
                _repository.AddCountry(country);
                return CreatedAtAction(nameof(Get), new { Id = country.Id }, country);
            }
        }

        // PATCH: Tries to execute a PARTIAL update of a resource
        [HttpPatch("{id}")]
        public ActionResult<Country> Patch(long id, [FromBody] JsonPatchDocument<Country> countryPatchDoc)
        {
            if (countryPatchDoc == null) return BadRequest();

            try
            {
                Country c = _repository.GetCountry(id);
                countryPatchDoc.ApplyTo(c); // country = array of patches to execute (included in the request body)
                return c;
            }
            catch (CountryException ex)
            {
                Console.WriteLine("Error during PATCH request: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during PATCH request: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _repository.RemoveCountry(_repository.GetCountry(id));
                return NoContent();
            }
            catch(CountryException ex)
            {
                return NotFound("Error during DELETE request: " + ex.Message);
            }
        }
    }
}
