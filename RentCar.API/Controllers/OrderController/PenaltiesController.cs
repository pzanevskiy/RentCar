using Microsoft.AspNetCore.Mvc;
using RentCar.API.Models;
using RentCar.API.Models.Request;
using RentCar.Database;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentCar.API.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenaltiesController : ControllerBase
    {
        private readonly RentCarDbContext _dbContext;

        public PenaltiesController(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Penalty.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _dbContext.Penalty.FirstOrDefault(x => x.PenaltyId == id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromForm] PostPenaltyModelRequest model)
        {
            _ = _dbContext.Penalty.Add(new Database.Entities.OrderEntities.Penalty()
            {
                Description = model.Description,
                PenaltyCost = model.PenaltyCost
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
            var result = _dbContext.Penalty.FirstOrDefault(x => x.PenaltyId == id);
            _ = _dbContext.Penalty.Remove(result);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
