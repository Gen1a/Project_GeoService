//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Project_GeoService.Controllers;
//using Project_GeoService.Exceptions;
//using Project_GeoService.Interfaces;
//using Project_GeoService.Models;
//using System;
//using Xunit;

//namespace Project_GeoService.xUnitTest
//{
//    public class CountryControllerTest
//    {
//        private readonly Mock<ICountryRepository> _mockRepo;
//        private readonly CountryController _countryController;

//        public CountryControllerTest()
//        {
//            _mockRepo = new Mock<ICountryRepository>();
//            _countryController = new CountryController(_mockRepo.Object);
//        }

//        [Fact]
//        public void GET_UknownID_ReturnsNotFound()
//        {
//            _mockRepo.Setup(repo => repo.GetCountry(2)).Throws(new CountryException());
//            ActionResult<Country> response = _countryController.Get(2);

//            Assert.IsType<NotFoundObjectResult>(response.Result);
//        }

//        [Fact]
//        public void GET_ValidID_ReturnsOkResult()
//        {
//            _mockRepo.Setup(repo => repo.GetCountry(3)).Returns(new Country(3, "Duitsland", "Berlijn", "Europa"));
//            ActionResult<Country> response = _countryController.Get(3);

//            Assert.IsType<Country>(response.Value);
//        }

//        [Fact]
//        public void GET_ValidID_ReturnsOkCountry()
//        {
//            Country c = new Country(3, "Duitsland", "Berlijn", "Europa");
//            _mockRepo.Setup(repo => repo.GetCountry(3)).Returns(c);
//            ActionResult<Country> response = _countryController.Get(3);

//            Assert.IsType<Country>(response.Value);
//            Assert.Equal(3, (response.Value as Country).Id);
//            Assert.Equal(c.Capital, (response.Value as Country).Capital);
//            Assert.Equal(c.Continent, (response.Value as Country).Continent);
//            Assert.Equal(c.Id, (response.Value as Country).Id);
//            Assert.Equal(c.Name, (response.Value as Country).Name);
//        }

//        [Fact]
//        public void POST_ValidObject_ReturnsCreatedAtAction()
//        {
//            Country c = new Country(6, "Finland", "Helsinki", "Europa");
//            ActionResult<Country> response = _countryController.Post(c);

//            Assert.IsType<CreatedAtActionResult>(response.Result);
//        }

//        [Fact]
//        public void POST_ValidObject_ReturnsOkCountry()
//        {
//            Country country = new Country(6, "Finland", "Helsinki", "Europa");
//            CreatedAtActionResult response = _countryController.Post(country).Result as CreatedAtActionResult;
//            Country c = response.Value as Country;

//            Assert.IsType<Country>(c);
//            Assert.Equal(country.Capital, c.Capital);
//            Assert.Equal(country.Continent, c.Continent);
//            Assert.Equal(country.Name, c.Name);
//            Assert.Equal(country.Id, c.Id);
//        }

//        [Fact]
//        public void POST_InvalidObject_ReturnsBadRequest()
//        {
//            Country country = new Country(6, "Finland", "Helsinki", "Europa");
//            _countryController.ModelState.AddModelError("Format error", "long expected");
//            ActionResult response = _countryController.Post(country).Result;

//            Assert.IsType<BadRequestObjectResult>(response);
//        }
//    }
//}
