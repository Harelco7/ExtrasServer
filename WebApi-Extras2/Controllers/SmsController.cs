using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CLExtras2.Models;
using Microsoft.AspNetCore.Cors;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;



namespace WebApi_Extras2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        Extras2Context db = new Extras2Context();

        private readonly SmsService _smsService;

        public SmsController(SmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpPost]
        [Route("api/sendOrderConfirmation")]
        public IActionResult SendOrderConfirmation([FromBody] string phoneNumber)
        {
            try
            {
                _smsService.SendSms(phoneNumber, "New order has been placed!");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to send SMS: " + ex.Message);
            }
        }



    }
    public class SmsRequest
    {
       
        public string Message { get; set; }

        public int BusinessID { get; set; }

    }
}
