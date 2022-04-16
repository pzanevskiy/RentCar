using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Database;
using RentCar.Database.Entities.CarEntities;

namespace RentCar.API.Controllers.CarControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTypesController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public CarTypesController(RentCarDbContext context)
        {
            _context = context;
        }

        // GET: api/CarTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarType>>> GetCarType()
        {
            return await _context.CarType.ToListAsync();
        }

        // GET: api/CarTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarType>> GetCarType(Guid id)
        {
            var carType = await _context.CarType.FindAsync(id);

            if (carType == null)
            {
                return NotFound();
            }

            return carType;
        }

        // PUT: api/CarTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarType(Guid id, CarType carType)
        {
            if (id != carType.CarTypeId)
            {
                return BadRequest();
            }

            _context.Entry(carType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarTypeExists(id))
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

        // POST: api/CarTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "rentcar_admin")]
        [HttpPost]
        public async Task<ActionResult<CarType>> PostCarType([FromForm]CarType carType)
        {
            _context.CarType.Add(carType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarType", new { id = carType.CarTypeId }, carType);
        }

        // DELETE: api/CarTypes/5
        [Authorize(Roles = "rentcar_admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarType>> DeleteCarType(Guid id)
        {
            var carType = await _context.CarType.FindAsync(id);
            if (carType == null)
            {
                return NotFound();
            }

            _context.CarType.Remove(carType);
            await _context.SaveChangesAsync();

            return carType;
        }

        private bool CarTypeExists(Guid id)
        {
            return _context.CarType.Any(e => e.CarTypeId == id);
        }
    }
}
