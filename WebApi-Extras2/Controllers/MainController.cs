using CLExtras2.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Extras2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class MainController : ControllerBase
    {
        Extras2Context db = new Extras2Context();

        [HttpGet]
        [Route("Businesses")]
        public dynamic GetAllBusinesses()
        {
            try
            {
                var businesses = db.Businesses.Select(b => new
                {
                    BusinessName = b.BusinessName,
                    BusinessType = b.BusinessType,
                    ContactInfo = b.ContactInfo,
                    Latitude = b.Latitude,
                    Longitude = b.Longitude,
                    BusinessPhoto = b.BusinessPhoto,
                    BusinessLogo = b.BusinessLogo,
                    OpeningHours = b.OpeningHours,
                    DailySalesHour = b.DailySalesHour,
                    BusinessAdress = b.Address,
                    BusinessID = b.BusinessId
                }).ToList();
                return businesses;
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
