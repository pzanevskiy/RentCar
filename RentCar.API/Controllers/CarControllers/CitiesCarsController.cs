using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Database;
using RentCar.Database.Entities.CarEntities;

namespace RentCar.API.Controllers.CarControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesCarsController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public CitiesCarsController(RentCarDbContext context)
        {
            _context = context;
        }

        // GET: api/CitiesCars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitiesCars>>> GetCitiesCars()
        {
            return await _context.CitiesCars.ToListAsync();
        }

        // GET: api/CitiesCars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitiesCars>> GetCitiesCars(Guid id)
        {
            var citiesCars = await _context.CitiesCars.FindAsync(id);

            if (citiesCars == null)
            {
                return NotFound();
            }

            return citiesCars;
        }

        // PUT: api/CitiesCars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitiesCars(Guid id, CitiesCars citiesCars)
        {
            if (id != citiesCars.CitiesCarId)
            {
                return BadRequest();
            }

            _context.Entry(citiesCars).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitiesCarsExists(id))
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

        // POST: api/CitiesCars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CitiesCars>> PostCitiesCars([FromForm] CitiesCars citiesCars)
        {
            _context.CitiesCars.Add(citiesCars);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitiesCars", new { id = citiesCars.CitiesCarId }, citiesCars);
        }

        // DELETE: api/CitiesCars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CitiesCars>> DeleteCitiesCars(Guid id)
        {
            var citiesCars = await _context.CitiesCars.FindAsync(id);
            if (citiesCars == null)
            {
                return NotFound();
            }

            _context.CitiesCars.Remove(citiesCars);
            await _context.SaveChangesAsync();

            return citiesCars;
        }

        private bool CitiesCarsExists(Guid id)
        {
            return _context.CitiesCars.Any(e => e.CitiesCarId == id);
        }
    }
}
