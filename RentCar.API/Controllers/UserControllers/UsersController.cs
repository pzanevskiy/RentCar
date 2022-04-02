using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.API.Models;
using RentCar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentCar.API.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RentCarDbContext _dbContext;

        public UsersController(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _dbContext.CarModel.Include(x => x.Brand).Select(x => x).Take(100)
                .Where(x => new string[] { "Audi", "Mercedes", "BMW" }.Contains(x.Brand.BrandName))
                .GroupBy(x => x.Brand.BrandName)
                .Select(x => new { Brand = x.Key, Count = x.Count() })
                .OrderBy(x => x.Brand);
            var x = _dbContext.User
                .Select(x => new { x.UserId, x.FirstName, x.LastName, x.PhoneNumber, x.Email })
                .ToList();
            return Ok(res);
        }

        [HttpGet("{id}/roles")]
        public IActionResult GetUserRoles(Guid id)
        {
            var result = _dbContext.UsersRoles
                .Where(x => x.UserId == id)
                .Select(x => new
                {
                    UserId = x.UserId,
                    Name = _dbContext.Role.First(y => y.RoleId == x.RoleId).Name
                })
                .ToList()
                .GroupBy(x => x.UserId).Select(x => new { User = x.Key, Roles = x.ToList().Select(x => x.Name) });
            //.ToDictionary(x => x.Key, x => x.ToList().Select(x => _dbContext.Role.FirstOrDefault(y => y.RoleId == x.RoleId)));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_dbContext.User.FirstOrDefault(x => x.UserId == id));
        }

        [HttpPost]
        public IActionResult Post([FromForm] PostUserModelRequest model)
        {
            _ = _dbContext.User.Add(new Database.Entities.UserEntities.User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
            });
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("user/{roleId}")]
        public IActionResult AssignUserToRole([FromBody] Guid userId, Guid roleId)
        {
            _ = _dbContext.UsersRoles.Add(new Database.Entities.UserEntities.UsersRoles()
            {
                UserId = userId,
                RoleId = roleId
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
            var result = _dbContext.User.FirstOrDefault(x => x.UserId == id);
            _ = _dbContext.User.Remove(result);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
