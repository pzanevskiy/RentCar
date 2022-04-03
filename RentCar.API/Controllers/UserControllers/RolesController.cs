using Microsoft.AspNetCore.Mvc;
using RentCar.API.Models;
using RentCar.Database;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentCar.API.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RentCarDbContext _dbContext;

        public RolesController(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Role.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_dbContext.Role.FirstOrDefault(x => x.RoleId == id));
        }

        [HttpPost]
        public IActionResult Post([FromForm] PostRoleModelRequest model)
        {
            _ = _dbContext.Role.Add(new Database.Entities.UserEntities.Role()
            {
                Name = model.Name
            });
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromForm] string value)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _dbContext.Role.FirstOrDefault(x => x.RoleId == id);
            _ = _dbContext.Role.Remove(result);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
