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
    public class RegisterController : ControllerBase
    {
        Extras2Context db = new Extras2Context();


        [HttpPost]
        [Route("registerClient")]
        public IActionResult RegisterClient([FromBody] UserCDTO client)
        {

            try
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Username == client.Username);
                if (existingUser != null)
                {
                    return Conflict("Username already exists.");
                }

                var user = new User
                {
                    Username = client.Username,
                    Password = client.Password,
                    UserType = "Client",
                    Address = client.Address,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber
                };

                db.Users.Add(user);
                db.SaveChanges();

                var customerId = user.UserId;

                var customer = new Customer
                {
                    UserId = (int)customerId,
                    CustomerName = client.CustomerName,
                    Age = (int)client.Age,
                    Gender = client.Gender,
                };

                db.Customers.Add(customer);
                db.SaveChanges();

                return Ok("Client registered successfully!");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("registerBusiness")]
        public IActionResult RegisterBusiness([FromBody] UserBDTO business)
        {

            try
            {

                var user = new User
                {
                    Username = business.Username,
                    Password = business.Password,
                    UserType = "Business",
                    Address = business.Address,
                };

                db.Users.Add(user);
                db.SaveChanges();

                var userId = user.UserId;

                var store = new Business
                {
                    UserId = userId,
                    BusinessName = business.BusinessName,
                    BusinessType = business.BusinessType,
                    ContactInfo = business.ContactInfo,
                    Latitude = business.Latitude,
                    Longitude = business.Longitude,
                    BusinessPhoto = business.BusinessPhoto,
                    BusinessLogo = business.BusinessLogo,
                    OpeningHours = business.OpeningHours,
                    DailySalesHour = business.DailySalesHour,
                    Address = business.Address,
                };

                db.Businesses.Add(store);
                db.SaveChanges();

                return Ok("Business registered successfully!");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
