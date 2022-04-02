using Microsoft.AspNetCore.Mvc;
using RentCar.API.Models;
using RentCar.Database;
using RentCar.Database.Entities.CarEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentCar.API.Controllers.CarControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly RentCarDbContext _dbContext;
        public ModelsController(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<ModelsController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _dbContext.CarModel.ToList();
            return Ok(result);
        }

        // GET api/<ModelsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _dbContext.CarModel.FirstOrDefault(x => x.ModelId == id);
            return Ok(result);
        }

        // POST api/<ModelsController>
        [HttpPost]
        public IActionResult Post([FromForm] PostCarModelRequest model)
        {
            _ = _dbContext.CarModel.Add(new CarModel()
            {
                ModelName = model.Model,
                BrandId = model.BrandId
            });
            _dbContext.SaveChanges();
            return Ok();
        }

        // PUT api/<ModelsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromForm] PostCarModelRequest model)
        {
            var result = _dbContext.CarModel.FirstOrDefault(x => x.ModelId == id);
            result.ModelName = model.Model;
            _ = _dbContext.CarModel.Update(result);
            _dbContext.SaveChanges();
            return Ok();
        }

        // DELETE api/<ModelsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _dbContext.CarModel.FirstOrDefault(x => x.ModelId == id);
            _ = _dbContext.CarModel.Remove(result);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
