using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.API.Models;
using RentCar.API.Models.Response.Location;
using RentCar.Database;
using RentCar.Database.Entities.LocationEntities;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("tree")]
        public async Task<IActionResult> GetCountriesTreeView()
        {
            var result = await _dbContext.Country.Include(x => x.Cities).ThenInclude(x => x.Addresses)
                .Select(x => new CountryViewModel
                {
                    CountryId = x.CountryId,
                    CountryName = x.CountryName,
                    Cities = x.Cities.Select(y => new CityViewModel
                    {
                        CityId = y.CityId,
                        CityName = y.CityName,
                        Addresses = y.Addresses.Select(z => new AddressViewModel
                        {
                            AddressId = z.OrderAddressId,
                            Addresss = z.OrderAddressName
                        })
                    }),
                }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult Get(Guid id)
        {
            var result = _dbContext.Country.FirstOrDefault(x => x.CountryId == id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromForm] PostCountryModelRequest model)
        {
            _dbContext.Country.Add(new Country()
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
