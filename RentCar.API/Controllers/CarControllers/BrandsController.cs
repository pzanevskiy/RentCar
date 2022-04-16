using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Database;
using RentCar.Database.Entities.CarEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentCar.API.Controllers.CarControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly RentCarDbContext _dbContext;
        public BrandsController(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [Authorize(Roles = "rentcar_admin")]
        [HttpGet]
        [Produces(typeof(IEnumerable<Brand>))]
        public IActionResult Get()
        {
            var x = HttpContext.Request.Headers["Authorization"].ToString().Split(" ");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(x[1]);
            var result = _dbContext.Brand.ToList();
            return Ok(new
            {
                Token = token.Audiences,
                result
            });
        }

        [Authorize(Roles = "rentcar_admin")]
        [HttpGet("/api/brands/models")]
        public IActionResult GetCarsWithModels()
        {
            var result = _dbContext.Brand.Select(x => new { x.BrandName, x.Models }).ToList();
            return Ok(result);
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        [Produces(typeof(Brand))]
        public IActionResult Get(Guid id)
        {
            var result = _dbContext.Brand.Where(x => x.BrandId.Equals(id));
            return Ok(result);
        }

        // POST api/<BrandsController>
        [Authorize(Roles = "rentcar_admin")]
        [HttpPost]
        public IActionResult Post([FromForm] Brand brand)
        {
            _ = _dbContext.Brand.Add(brand);
            _dbContext.SaveChanges();
            return Ok();
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromForm] string name)
        {
            var brand = _dbContext.Brand.FirstOrDefault(x => x.BrandId.Equals(id));
            brand.BrandName = name;
            _ = _dbContext.Brand.Update(brand);
            _dbContext.SaveChanges();
            return Ok();
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var brand = _dbContext.Brand.FirstOrDefault(x => x.BrandId.Equals(id));
            _ = _dbContext.Brand.Remove(brand);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
