using Microsoft.AspNetCore.Mvc;
using RentCar.API.Models;
using RentCar.API.Models.Request;
using RentCar.Database;
using RentCar.Database.Entities.OrderEntities;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentCar.API.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnhancementsController : ControllerBase
    {
        private readonly RentCarDbContext _dbContext;

        public EnhancementsController(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Enhancement.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _dbContext.Enhancement.FirstOrDefault(x => x.EnhancementId == id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromForm] PostEnhancementModelRequest model)
        {
            _ = _dbContext.Enhancement.Add(new Enhancement()
            {
                Description = model.Description,
                Price = model.Price
            });
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] string value)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _dbContext.Enhancement.FirstOrDefault(x => x.EnhancementId == id);
            _ = _dbContext.Enhancement.Remove(result);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
