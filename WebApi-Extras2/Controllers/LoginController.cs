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
    public class LoginController : ControllerBase
    {
        Extras2Context db = new Extras2Context();

        [HttpPost]
        [Route("LoginTest")]
        public IActionResult Authenticate([FromBody] LoginDTO login)
        {
            // Fetch user
            try
            {
                // Fetch user
                var user = db.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);

                if (user == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                if (user.UserType == "Client")
                {

                    var client = db.Customers.FirstOrDefault(c => c.UserId == user.UserId);
                    if (client == null)
                    {
                        // Handle case where client is null
                        return NotFound("Client data not found.");
                    }

                    UserCDTO cDTO = new UserCDTO
                    {
                        Username = user.Username,
                        Address = user.Address,
                        CustomerName = client.CustomerName,
                        Age = client.Age,
                        Gender = client.Gender,
                        CustomerID =client.CustomerId,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        UserID = user.UserId,
                        
                    };

                    return Ok(cDTO);
                }
                else if (user.UserType == "Business")
                {
                    var business = db.Businesses.FirstOrDefault(b => b.UserId == user.UserId);
                    if (business == null)
                    {
                        return NotFound("Business data not found.");
                    }

                    // Construct business DTO
                    var bDTO = new UserBDTO
                    {
                        Username = user.Username,
                        Address = user.Address,
                        BusinessName = business.BusinessName,
                        BusinessType = business.BusinessType,
                        ContactInfo = business.ContactInfo,
                        Latitude = business.Latitude,
                        Longitude = business.Longitude,
                        BusinessPhoto = business.BusinessPhoto,
                        BusinessLogo = business.BusinessLogo,
                        OpeningHours = business.OpeningHours,
                        DailySalesHour = business.DailySalesHour,
                        BusinessID = business.BusinessId
                    };

                    return Ok(bDTO);
                }

                return BadRequest("User Not Exists");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
