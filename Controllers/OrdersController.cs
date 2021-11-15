using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend1_2.Data;
using Backend1_2.Entities;
using Backend1_2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Dbfirst_ehandel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrdersController(SqlContext context)
        {
            _context = context;
        }




        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrder>>> GetOrders()
        {
            var orders = await _context.Orders.Include(x => x.OrderLines).ToListAsync();
            var _orders = new List<GetOrder>();



            foreach (var order in orders)
            {
                var nextOrder = new GetOrder();
                nextOrder.Id = order.Id;
                nextOrder.UserId = order.UserId;
                nextOrder.OrderDate = order.OrderDate;
                nextOrder.Status = order.Status;
                nextOrder.OrderLine = new();



                foreach (var line in order.OrderLines)
                {
                    GetOrderLine ol = new();
                    ol.ProductId = line.ProductId;
                    ol.UnitPrice = line.UnitPrice;
                    ol.Quantity = line.Quantity;



                    nextOrder.OrderLine.Add(ol);
                }
                _orders.Add(nextOrder);
            }
            return _orders;
        }





        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrder>> GetOrder(int id)
        {
            var orders = await _context.Orders.Include(x => x.OrderLines).ToListAsync();
            if(orders != null)
            {

                foreach (var order in orders)
                {
                    if (order.Id == id)
                    {
                        var nextOrder = new GetOrder();
                        nextOrder.Id = order.Id;
                        nextOrder.UserId = order.UserId;
                        nextOrder.OrderDate = order.OrderDate;
                        nextOrder.Status = order.Status;
                        nextOrder.OrderLine = new();



                        foreach (var line in order.OrderLines)
                        {
                            GetOrderLine ol = new();
                            ol.ProductId = line.ProductId;
                            ol.UnitPrice = line.UnitPrice;
                            ol.Quantity = line.Quantity;



                            nextOrder.OrderLine.Add(ol);
                        }
                        return nextOrder;

                    }


                }
                return NotFound();
            }
            else
            {
                return NotFound();



            } 
    }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, UpdateOrder order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var _order = await _context.Orders.FindAsync(id);

            if (_order == null)
            {
                return NotFound();
            }
            if (_order.Status == "received")
            {
                _order.Status = "sent";
            } else if (_order.Status == "sent") {
                _order.Status = "received";
            }else
            {
                return BadRequest();
            }


            _context.Entry(_order).State = EntityState.Modified;

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(CreateOrder model)
        {
            var order = new Order()
            {
                UserId = model.UserId,
                OrderDate = model.OrderDate,
                Status = "received",
                OrderLines = new List<OrderLine>()
            };

            foreach (var line in model.OrderLine)
            {
                OrderLine ol = new();
                ol.ProductId = line.ProductId;
                ol.UnitPrice = line.UnitPrice;
                ol.Quantity = line.Quantity;

                order.OrderLines.Add(ol);
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id , UpdateOrder model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }



            var _order = await _context.Orders.FindAsync(id);
            if (_order == null)
            {
                return NotFound();
            }
            if (_order.Status == "deleted")
            {
                return BadRequest();
            }
            else
            {
                _order.Status = "deleted";
            }



            _context.Entry(_order).State = EntityState.Modified;



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
        

private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
