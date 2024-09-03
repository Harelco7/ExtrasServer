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
    public class UserController : ControllerBase
    {
        Extras2Context db = new Extras2Context();


        [HttpPost]
        [Route("updateFavorite")]
        public IActionResult UpdateFavorite([FromBody] FavoriteDTO favorite)
        {
            try
            {
                Favorite f;

                if (favorite.IsFavorite == "yes")
                {
                    f = new Favorite() { CustomerId = favorite.CustomerId, BusinessId = favorite.BusinessID };
                    db.Favorites.Add(f);
                }
                else
                {
                    if (favorite.BusinessID != null && favorite.CustomerId != null)
                    {
                        f = db.Favorites.Where(f => f.BusinessId == favorite.BusinessID && f.CustomerId == favorite.CustomerId).SingleOrDefault();
                        db.Favorites.Remove(f);
                    }

                }

                db.SaveChanges();

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getFavorite/{CustomerId}/{BusinessDetails}")]
        public IActionResult GetFavorite(int CustomerId, string BusinessDetails)
        {
            try
            {
                List<Favorite> f;
                f = db.Favorites.Where(f => f.CustomerId == CustomerId).ToList();
                if (BusinessDetails == "yes")
                {

                    List<int> ids = f.Select(f => f.BusinessId).ToList();
                    List<Business> businesses = db.Businesses.Where(b => ids.Contains(b.BusinessId)).ToList();
                    return Ok(businesses);
                }

                return Ok(f);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Route("UpdateUserDetails/{customerID}")]
        public IActionResult UpdateCustomer(int customerID, [FromBody] UpdateCustomerDTO customer2Update)
        {
            try
            {
                var customer = db.Customers.FirstOrDefault(c => c.CustomerId == customerID);
                var user = db.Users.FirstOrDefault(c => c.UserId == customer2Update.UserId);

                if (customer == null)
                {
                    return NotFound("customer not found");
                }

                user.PhoneNumber = customer2Update.PhoneNumber;
                user.Address = customer2Update.Address;
                user.Email = customer2Update.Email;
                customer.CustomerName = customer2Update.CustomerName;


                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDeliveredOrders/{CustomerId}")]
        public IActionResult GetDeliveredOrders(int CustomerId)
        {
            try
            {
                
                var orders = db.Orders
                               .Where(o => o.CustomerId == CustomerId && o.OrderStatus == "Delivered")
                               .ToList();

                if (orders == null || orders.Count == 0)
                {
                    return NotFound("No delivered orders found for the specified customer.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

    }
}