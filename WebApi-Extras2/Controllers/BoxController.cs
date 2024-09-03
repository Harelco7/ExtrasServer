using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CLExtras2.Models;
using Microsoft.AspNetCore.Cors;
using WebApi_Extras2.DTO_s;
using System;
using Microsoft.Extensions.Logging;  // For logging purposes

namespace WebApi_Extras2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class BoxController : ControllerBase
    {
        private readonly Extras2Context _db;
        private readonly SmsService _smsService;  // Assuming SmsService is already configured for DI
        private readonly ILogger<BoxController> _logger;  // Logger instance

        // Constructor with Extras2Context and SmsService injected
        public BoxController(Extras2Context db, SmsService smsService, ILogger<BoxController> logger)
        {
            _db = db;
            _smsService = smsService;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddBox")]
        public IActionResult AddBox([FromBody] BoxDTO boxFromBusiness)
        {
            try
            {
                var box = new Box
                {
                    BoxName = boxFromBusiness.BoxName,
                    Description = boxFromBusiness.Description,
                    Price = boxFromBusiness.Price,
                    Sale_Price = boxFromBusiness.Sale_Price,
                    QuantityAvailable = boxFromBusiness.QuantityAvailable,
                    DateAdded = DateTime.Today,
                    BusinessId = boxFromBusiness.BusinessID,
                    AlergicType = boxFromBusiness.AlergicType,
                    BoxId = boxFromBusiness.BoxId
                };

                _db.Boxes.Add(box);
                _db.SaveChanges();

                return Ok(new { Message = "Box added successfully", BoxDetails = box });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding box");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetBox/{boxId}")]
        public IActionResult GetBoxById(int boxId)
        {
            try
            {
                var box = _db.Boxes.FirstOrDefault(b => b.BoxId == boxId);
                if (box == null)
                {
                    _logger.LogInformation($"Box with ID {boxId} not found.");
                    return NotFound(new { Message = "Box not found" });
                }

                _logger.LogInformation($"Retrieved box with ID {boxId}.");
                return Ok(box);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving box with ID {boxId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpPut]
        [Route("BuyBox")]
        public IActionResult BuyBox([FromBody] BuyBoxDTO boxClientBuys)
        {
            if (boxClientBuys == null)
            {
                return BadRequest("Invalid data provided");
            }

            var box = _db.Boxes.FirstOrDefault(b => b.BoxId == boxClientBuys.BoxId);
            if (box == null)
            {
                return NotFound("Box not found");
            }

            if (box.QuantityAvailable < boxClientBuys.QuantityBuy)
            {
                return BadRequest("Insufficient quantity available");
            }

            box.QuantityAvailable -= boxClientBuys.QuantityBuy;

            var order = new Order
            {
                CustomerId = boxClientBuys.CustomerId,
                OrderDate = DateTime.Now,
                QuantityOrdered = boxClientBuys.QuantityBuy,
                TotalPrice = box.Sale_Price * boxClientBuys.QuantityBuy,
                OrderStatus = "Pending",
                BoxID = boxClientBuys.BoxId,
                boxDescription = boxClientBuys.boxDescription,
                BusinessID = boxClientBuys.businessID,
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            var businessNumber = _db.Businesses.Where(b => b.BusinessId==order.BusinessID).FirstOrDefault();
            string businessnumber2send = businessNumber.ContactInfo;
            // Sending SMS Notification
            _smsService.SendSms("+972522233080", $"שלום! התקבלה הזמנה חדשה לבית העסק שלך! - ההזמנה :  {order.QuantityOrdered} {box.BoxName} ");

            return Ok(new { Message = $"Order placed successfully. Updated Quantity to: {box.QuantityAvailable}", OrderDetails = order });
        }
    }
}
