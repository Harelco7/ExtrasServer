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
    public class BusinessController : ControllerBase
    {
        Extras2Context db = new Extras2Context();

        [HttpGet]
        [Route("GetBusiness/{businessId}")]
        public IActionResult GetBusiness(int businessId)
        {
            try
            {
                var business = db.Businesses
                    .Where(b => b.BusinessId == businessId)
                    .Select(b => new
                    {
                        BusinessId = b.BusinessId,
                        BusinessName = b.BusinessName,
                        BusinessType = b.BusinessType,
                        ContactInfo = b.ContactInfo,
                        DailySalesHour = b.DailySalesHour,
                        OpeningHours = b.OpeningHours,
                        businessPhoto = b.BusinessPhoto,
                        businesslogo = b.BusinessLogo,
                        businessAdress= b.Address,
                        BusinessDescription= b.Business_description
                    })
                    .FirstOrDefault();

                if (business == null)
                {
                    return NotFound("Business not found");
                }

                return Ok(business);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ShowBoxes/{businessId}")]
        public IActionResult GetBoxesForBusiness(int businessId)
        {
            try
            {
                var boxes = db.Boxes
                    .Where(box => box.BusinessId == businessId)
                    .Select(box => new
                    {
                        BoxId = box.BoxId,
                        BoxName = box.BoxName,
                        Description = box.Description,
                        Price = box.Price,
                        QuantityAvailable = box.QuantityAvailable,
                        BoxImage = box.BoxImage,
                        AlergicType = box.AlergicType,
                        businessID = box.BusinessId,
                        SalePrice = box.Sale_Price
                    }).ToList();

                if (!boxes.Any())
                {
                    return NotFound("No boxes found for this business");
                }

                return Ok(boxes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateBusiness/{businessId}")]
        public IActionResult UpdateBusiness(int businessId, [FromBody] UpdateBusinessDTO business2Update)
        {
            try
            {
                var business = db.Businesses.FirstOrDefault(b => b.BusinessId == businessId);
                if (business == null)
                {
                    return NotFound("Business not found");
                }

                business.BusinessName = business2Update.BusinessName;
                business.BusinessType = business2Update.BusinessType;
                business.ContactInfo = business2Update.ContactInfo;
                business.DailySalesHour = business2Update.DailySalesHour;
                business.OpeningHours = business2Update.OpeningHours;

                db.SaveChanges();
                return Ok(business);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
