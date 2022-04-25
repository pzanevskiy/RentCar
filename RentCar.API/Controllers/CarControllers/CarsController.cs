using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.API.Models.Request;
using RentCar.API.Models.Response;
using RentCar.API.Models.Response.Car;
using RentCar.Database;
using RentCar.Database.Entities.CarEntities;

namespace RentCar.API.Controllers.CarControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public CarsController(RentCarDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<IActionResult> GetCar()
        {
            var cars = await _context.Car
                .Include(x => x.CarType)
                .Include(x => x.Model).ThenInclude(x => x.Brand)
                .Select(x => new CarViewModel
                {
                    CarId = x.CarId,
                    Brand = x.Model.Brand.BrandName,
                    Model = x.Model.ModelName,
                    Type = x.CarType.TypeName,
                    Transmission = x.Transmission,
                    DoorsCount = x.DoorsCount,
                    SeatsCount = x.SeatsCount,
                    BagsCount = x.BagsCount,
                    AC = x.AC
                })
                .ToListAsync();

            return Ok(cars);
        }

        [HttpGet("/api/Cars/all")]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _context.Car.ToListAsync();

            return Ok(cars);
        }

        [Authorize(Roles = "rentcar_user, rentcar_admin")]
        [HttpGet("/api/Cars/city/{cityId}")]
        public async Task<IActionResult> GetCarByCityId([FromRoute] Guid cityId)
        {
            var cars = await _context.Car
                .Include(x => x.CitiesCars)
                .Include(x => x.CarType)
                .Include(x => x.Model).ThenInclude(x => x.Brand)
                .Where(x => x.CitiesCars.CityId == cityId)
                .Select(x => new CarPriceViewModel
                {
                    CarId = x.CarId,
                    Brand = x.Model.Brand.BrandName,
                    Model = x.Model.ModelName,
                    Type = x.CarType.TypeName,
                    Transmission = x.Transmission,
                    DoorsCount = x.DoorsCount,
                    SeatsCount = x.SeatsCount,
                    BagsCount = x.BagsCount,
                    AC = x.AC,
                    Price = x.CitiesCars.Price
                })
                .ToListAsync();

            return Ok(new GetCarsResponse { Cars = cars });
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(Guid id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(new CarViewModel
            {
                CarId = car.CarId,
                Brand = car.Model.Brand.BrandName,
                Model = car.Model.ModelName,
                Type = car.CarType.TypeName,
                Transmission = car.Transmission,
                DoorsCount = car.DoorsCount,
                SeatsCount = car.SeatsCount,
                BagsCount = car.BagsCount,
                AC = car.AC
            });
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar([FromForm] PostCarRequest carRequest)
        {
            var modelId = _context.CarModel.Include(x => x.Brand)
                .FirstOrDefault(x => x.ModelName == carRequest.Model &&
                    x.Brand.BrandName == carRequest.Brand).ModelId;
            var typeId = _context.CarType.FirstOrDefault(x => x.TypeName == carRequest.Type).CarTypeId;

            var car = new Car
            {
                ModelId = modelId,
                CarTypeId = typeId,
                Transmission = carRequest.Transmission,
                DoorsCount = carRequest.DoorsCount,
                SeatsCount = carRequest.SeatsCount,
                BagsCount = carRequest.BagsCount,
                AC = carRequest.AC
            };
            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(Guid id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(Guid id)
        {
            return _context.Car.Any(e => e.CarId == id);
        }
    }
}
