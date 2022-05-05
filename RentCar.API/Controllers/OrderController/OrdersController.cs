﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
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
                TotalAmount = total
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
