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
    public class CountryController : ControllerBase
    {
        private readonly RentCarDbContext _dbContext;

        public CountryController(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _dbContext.Country.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _dbContext.Country.FirstOrDefault(x => x.CountryId == id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromForm] PostCountryModelRequest model)
        {
            _dbContext.Country.Add(new Database.Entities.CarEntities.Country()
            {
                CountryName = model.CountryName
            });
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _dbContext.Country.FirstOrDefault(x => x.CountryId == id);
            _ = _dbContext.Country.Remove(result);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
