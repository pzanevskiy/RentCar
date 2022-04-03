using Microsoft.AspNetCore.Mvc;
using RentCar.API.Models;
using RentCar.Database;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentCar.API.Controllers.LocationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly RentCarDbContext _dbContext;

        public CitiesController(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.City.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _dbContext.City.FirstOrDefault(x => x.CityId == id);
            return Ok(result);
        }

        // POST api/<CitiesController>
        [HttpPost]
        public IActionResult Post([FromForm] PostCityModelRequest model)
        {
            _ = _dbContext.City.Add(new Database.Entities.CarEntities.City()
            {
                CityName = model.CityName,
                CountryId = model.CountryId
            });
            _dbContext.SaveChanges();
            return Ok();
        }

        // PUT api/<CitiesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] string value)
        {
            return Ok();
        }

        // DELETE api/<CitiesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _dbContext.City.FirstOrDefault(x => x.CityId == id);
            _ = _dbContext.City.Remove(result);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
