using CLExtras2.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Extras2.DTO_s;

namespace WebApi_Extras2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class MyDataBusinessController : ControllerBase
    {
        Extras2Context db = new Extras2Context();


        [HttpGet("GetSalesData")]
        public IActionResult GetSalesData([FromQuery] int businessId)
        {
            var orders = db.Orders.Where(x => x.BusinessID == businessId);

            var totalBoxesOrdered = orders.Sum(x => x.QuantityOrdered);
            var totalPrice = orders.Sum(x => x.TotalPrice);
            var ordersPending = orders.Where(x => x.OrderStatus == "Pending").Count();

            var openOrders = orders.Where(x => x.OrderStatus == "Pending")
                                   .Join(db.Customers,
                                         order => order.CustomerId,
                                         customer => customer.CustomerId,
                                         (order, customer) => new
                                         {
                                             order.OrderId,
                                             order.CustomerId,
                                             CustomerName = customer.CustomerName,
                                             order.boxDescription,
                                             order.OrderStatus,
                                             order.OrderDate,
                                             order.TotalPrice,
                                             order.BoxID,
                                             order.QuantityOrdered,
                                         })
                                   .ToList();

            var deliveredOrders = orders.Where(x => x.OrderStatus == "Delivered")
                                        .Join(db.Customers,
                                              order => order.CustomerId,
                                              customer => customer.CustomerId,
                                              (order, customer) => new
                                              {
                                                  order.OrderId,
                                                  order.CustomerId,
                                                  CustomerName = customer.CustomerName,
                                                  order.boxDescription,
                                                  order.OrderStatus,
                                                  order.OrderDate,
                                                  order.TotalPrice,
                                                  order.BoxID,
                                                  order.QuantityOrdered,
                                                 
                                              })
                                        .ToList();

            var result = new
            {
                OrdersPending = ordersPending,
                TotalBoxesOrdered = totalBoxesOrdered,
                TotalPrice = totalPrice,
                OpenOrders = openOrders,
                DeliveredOrders = deliveredOrders
            };

            return Ok(result);
        }

        [HttpPut("UpdateOrderStatus")]
        [EnableCors("MyPolicy")]
        public IActionResult UpdateOrderStatus([FromBody] UpdateOrderStatusRequest request)
        {
            var orderdelivered = db.Orders.FirstOrDefault(x => x.OrderId == request.OrderId );
            if (orderdelivered == null)
            {
                return NotFound("Order not found.");
            }

            orderdelivered.OrderStatus = "Delivered";
            db.SaveChanges();

            return Ok("Order status updated to Delivered.");
        }

        public class UpdateOrderStatusRequest
        {
            public int OrderId { get; set; }
            
        }






    }
}
