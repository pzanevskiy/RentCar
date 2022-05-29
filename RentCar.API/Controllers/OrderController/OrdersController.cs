using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.API.Models.Request;
using RentCar.Database;
using RentCar.Database.Entities.OrderEntities;

namespace RentCar.API.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public OrdersController(RentCarDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order.ToListAsync();
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetOrderByUserId()
        {
            var userId = Guid.Parse(HttpContext.User.Claims
                .First(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var order = await _context.Order
                .Include(x => x.Car).ThenInclude(x => x.Model).ThenInclude(x => x.Brand)
                .Include(x => x.Car.CarType)
                .Include(x => x.PickUpAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.ReturnAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Where(x => x.Car.CitiesCars.Any(y => y.CityId == x.PickUpAddress.CityId))
                .Include(x => x.OrderStatus)
                .Include(x => x.Penalties)
                .Include(x => x.EnhancementsOrders).ThenInclude(x => x.Enhancement)
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.OrderId,
                    User = new { x.UserId },
                    x.OrderStatus.StatusName,
                    x.StartDate,
                    x.EndDate,
                    x.TotalAmount,
                    x.DateTimeCreated,
                    x.DateTimeFinished,
                    PickUpLocation = string.Join(", ",
                        x.PickUpAddress.City.Country.CountryName,
                        x.PickUpAddress.City.CityName,
                        x.PickUpAddress.OrderAddressName),
                    ReturnLocation = string.Join(", ",
                        x.ReturnAddress.City.Country.CountryName,
                        x.ReturnAddress.City.CityName,
                        x.ReturnAddress.OrderAddressName),
                    HasPenalties = x.Penalties.Any(),
                    Car = new
                    {
                        CarId = x.CarId,
                        Brand = x.Car.Model.Brand.BrandName,
                        Model = x.Car.Model.ModelName,
                        Type = x.Car.CarType.TypeName,
                        Price = x.Car.CitiesCars.FirstOrDefault(c => c.CarId == x.CarId).Price,
                        Transmission = x.Car.Transmission,
                        SeatsCount = x.Car.SeatsCount,
                        DoorsCount = x.Car.DoorsCount,
                        BagsCount = x.Car.BagsCount,
                        AC = x.Car.AC,
                        PictureLink = x.Car.PictureLink
                    },
                    Enhancements = x.EnhancementsOrders.Select(e => new
                    {
                        e.EnhancementId,
                        e.Enhancement.Description,
                        e.Enhancement.Price
                    })
                })
                .ToListAsync();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("{orderId}/cost")]
        public async Task<IActionResult> GetOrderDetailedCost([FromRoute] Guid orderId)
        {
            var order = await _context.Order
                .Include(x => x.Car).ThenInclude(x => x.Model).ThenInclude(x => x.Brand)
                .Include(x => x.Car.CarType)
                .Include(x => x.PickUpAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.ReturnAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Where(x => x.Car.CitiesCars.Any(y => y.CityId == x.PickUpAddress.CityId))
                .Include(x => x.OrderStatus)
                .Include(x => x.EnhancementsOrders).ThenInclude(x => x.Enhancement)
                .Where(x => x.OrderId == orderId)
                .Select(x => new
                {
                    x.OrderId,
                    User = new
                    {
                        x.UserId,
                        x.User.FirstName,
                        x.User.LastName,
                        x.User.PhoneNumber,
                        x.User.Email,
                    },
                    Car = new
                    {
                        CarId = x.CarId,
                        Brand = x.Car.Model.Brand.BrandName,
                        Model = x.Car.Model.ModelName,
                        Price = _context.CitiesCars.FirstOrDefault(c => c.CarId == x.CarId).Price,
                    },
                    Enhancements = x.EnhancementsOrders.Select(e => new
                    {
                        e.EnhancementId,
                        e.Enhancement.Description,
                        e.Enhancement.Price
                    })
                }).FirstOrDefaultAsync();

            return Ok(order);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
        {
            var order = await _context.Order
                .Include(x => x.Car).ThenInclude(x => x.Model).ThenInclude(x => x.Brand)
                .Include(x => x.Car.CarType)
                .Include(x => x.PickUpAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.ReturnAddress).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Where(x => x.Car.CitiesCars.Any(y => y.CityId == x.PickUpAddress.CityId))
                .Include(x => x.OrderStatus)
                .Include(x => x.Penalties)
                .Include(x => x.EnhancementsOrders).ThenInclude(x => x.Enhancement)
                .FirstOrDefaultAsync(x => x.OrderId == orderId);

            if (order == null)
            {
                return NotFound();
            }

            var res = new
            {
                order.OrderId,
                User = new { order.UserId },
                order.OrderStatus.StatusName,
                order.StartDate,
                order.EndDate,
                order.TotalAmount,
                order.DateTimeCreated,
                order.DateTimeFinished,
                PickUpLocation = string.Join(", ",
                        order.PickUpAddress.City.Country.CountryName,
                        order.PickUpAddress.City.CityName,
                        order.PickUpAddress.OrderAddressName),
                ReturnLocation = string.Join(", ",
                        order.ReturnAddress.City.Country.CountryName,
                        order.ReturnAddress.City.CityName,
                        order.ReturnAddress.OrderAddressName),
                HasPenalties = order.Penalties.Any(),
                Car = new
                {
                    CarId = order.CarId,
                    Brand = order.Car.Model.Brand.BrandName,
                    Model = order.Car.Model.ModelName,
                    Type = order.Car.CarType.TypeName,
                    Price = _context.CitiesCars.FirstOrDefault(c => c.CarId == order.CarId).Price,
                    Transmission = order.Car.Transmission,
                    SeatsCount = order.Car.SeatsCount,
                    DoorsCount = order.Car.DoorsCount,
                    BagsCount = order.Car.BagsCount,
                    AC = order.Car.AC,
                    PictureLink = order.Car.PictureLink
                },
                Enhancements = order.EnhancementsOrders.Select(e => new
                {
                    e.EnhancementId,
                    e.Enhancement.Description,
                    e.Enhancement.Price
                })
            };

            return Ok(res);
        }

        [Authorize(Roles = "rentcar_admin")]
        [HttpGet("admin/all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var order = await _context.Order
                .Include(x => x.User).ThenInclude(x => x.LoyaltyProgram)
                .Include(x => x.Penalties)
                .Include(x => x.OrderStatus)
                .Select(x => new
                {
                    User = new
                    {
                        x.User.UserId,
                        x.User.FirstName,
                        x.User.LastName,
                        x.User.PhoneNumber,
                        x.User.Email,
                        //Loyalty = new
                        //{
                        //    LoyaltyId = x.User.LoyaltyProgram.LoyatyId,
                        //    x.User.LoyaltyProgram.LoyaltyName,
                        //    x.User.LoyaltyProgram.Discount
                        //}
                    },
                    Order = new
                    {
                        x.OrderId,
                        x.OrderStatus.StatusName,
                        x.StartDate,
                        x.EndDate,
                        x.TotalAmount,
                        x.DateTimeCreated,
                        x.DateTimeFinished,
                        HasPenalties = x.Penalties.Any()
                    },
                })
                .ToListAsync();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("status/{id}/{statusName}")]
        public async Task<IActionResult> PutOrder(Guid id, string statusName)
        {
            var order = await _context.Order.FindAsync(id);

            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var orderStatus = await _context.OrderStatus
                .FirstOrDefaultAsync(x => x.StatusName == statusName);

            order.OrderStatusId = orderStatus.OrderStatusId;

            if (statusName == "Canceled" || statusName == "Completed" || statusName == "Rejected")
            {
                order.DateTimeFinished = DateTime.Now;
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostOrder(PostOrderRequest postOrderRequest)
        {
            var orderId = Guid.NewGuid();
            var userId = Guid.Parse(HttpContext.User.Claims
                .First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var orderStatusId = _context.OrderStatus
                .FirstOrDefault(x => x.StatusName == OrderStatusTypes.Pending).OrderStatusId;

            var selectedCar = await _context.CitiesCars
                .FirstOrDefaultAsync(x => x.CarId == postOrderRequest.CarId
                    && x.CityId == postOrderRequest.CityId);

            var enhancements = _context.Enhancement
                .Where(x => postOrderRequest.Enhancements.Any(y => y == x.EnhancementId));

            var totalDays = Math.Ceiling((postOrderRequest.EndDate - postOrderRequest.StartDate).TotalDays);

            var totalAmountPerDay = selectedCar.Price + enhancements.Sum(x => x.Price);

            var total = Math.Round(totalDays * totalAmountPerDay, 2);

            var order = new Order
            {
                OrderId = orderId,
                CarId = postOrderRequest.CarId,
                PickUpAddressId = postOrderRequest.PickUpAddressId,
                ReturnAddressId = postOrderRequest.ReturnAddressId,
                StartDate = postOrderRequest.StartDate,
                EndDate = postOrderRequest.EndDate,
                UserId = userId,
                OrderStatusId = orderStatusId,
                TotalAmount = total,
                DateTimeCreated = DateTime.Now
            };
            _context.Order.Add(order);

            foreach (var enh in postOrderRequest.Enhancements)
            {
                _context.EnhancementsOrders.Add(new EnhancementsOrders
                {
                    EnhancementId = enh,
                    OrderId = orderId
                });
            }
            await _context.SaveChangesAsync();

            return Ok(orderId);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(Guid id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
