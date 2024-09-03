using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System;
using System.Linq;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CLExtras2.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Hosting;


namespace WebApi_Extras2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class FileUploadController : ControllerBase
    {
        Extras2Context db = new Extras2Context();
        [HttpPost("uploadBusinessimage")]
        public async Task<IActionResult> UploadBusinessImage(IFormFile file, [FromForm] short businessid)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var BusinessImagePath = Path.Combine(Directory.GetCurrentDirectory(), "..//images//BusinessImage");

            if (!Directory.Exists(BusinessImagePath))
            {
                Directory.CreateDirectory(BusinessImagePath);
            }

            // Use the custom file name provided by the client
            var fileName = string.IsNullOrEmpty(businessid.ToString()) // שומר את זה בתור 8941.png
                ? file.FileName
                : $"{businessid}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(BusinessImagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Business business = db.Businesses.Where(x => x.BusinessId == businessid).First();
            business.BusinessPhoto = fileName;
            db.SaveChanges();

            return Ok(new { fileName = fileName, filePath = filePath });
        }

        

        [HttpPost("uploadLogoImage")]

        public async Task<IActionResult> UploadLogoImage(IFormFile file, [FromForm] short businessid)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var BusinessLogoPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "images", "BusinessLogo");


            if (!Directory.Exists(BusinessLogoPath))
            {
                Directory.CreateDirectory(BusinessLogoPath);
            }

            // Use the custom file name provided by the client
            var fileName = string.IsNullOrEmpty(businessid.ToString()) // שומר את זה בתור 8941.png
                ? file.FileName
                : $"{businessid}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(BusinessLogoPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Business business = db.Businesses.Where(x => x.BusinessId == businessid).First();
            business.BusinessLogo = fileName;
            db.SaveChanges();

            return Ok(new { fileName = fileName, filePath = filePath });
        }


        [HttpPost("uploadBoximage")]

        public async Task<IActionResult> UploadBoxImage(IFormFile file, [FromForm] short boxid)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var BoxImagePath = Path.Combine(Directory.GetCurrentDirectory(), "..//images//BoxImage");

            if (!Directory.Exists(BoxImagePath))
            {
                Directory.CreateDirectory(BoxImagePath);
            }

            // Use the custom file name provided by the client
            var fileName = string.IsNullOrEmpty(boxid.ToString()) // שומר את זה בתור 8941.png
                ? file.FileName
                : $"{boxid}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(BoxImagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Box box = db.Boxes.Where(x => x.BoxId == boxid).First();
            box.BoxImage = "~/BoxImage/" + fileName;
            db.SaveChanges();

            return Ok(new { fileName = fileName, filePath = filePath });
        }
        private readonly IWebHostEnvironment _environment;
    }

}

