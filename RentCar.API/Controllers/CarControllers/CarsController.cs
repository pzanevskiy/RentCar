using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Database;
using RentCar.Database.Entities.CarEntities;
using RentCar.Database.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentCar.API.Controllers.CarControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly RentCarDbContext _dbContext;

        public CarsController(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: api/<CarsController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _dbContext.Car.ToList();               
            return Ok(result);
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Car), 200)]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        // POST api/<CarsController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok();
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Guid), 200)]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
