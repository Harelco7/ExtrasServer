using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Extras2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class GetImageController : ControllerBase
    {

        [HttpGet]
        [Route("getAllBusinessImages")]
        public IActionResult GetAllBusinessImages()
        {
            var BusinessImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "BusinessImage");
            var files = Directory.GetFiles(BusinessImagesPath).Select(Path.GetFileName);

            if (!files.Any())
            {
                return NotFound("No images found.");
            }

            var urls = files.Select(file => Url.Content($"~/BusinessImage/{file}")).ToList();

            return Ok(urls);
        }





        [HttpGet]
        [Route("getBusinessImage/{businessid}")]
        public IActionResult GetBusinessImage(string businessid)
        {
            var BusinessImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "BusinessImage");
            var file = Directory.GetFiles(BusinessImagesPath, $"{businessid}.*").FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            var fileType = Path.GetExtension(file).ToLower();

            // Determine the content type based on the file extension.
            var contentType = fileType switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream",
            };

            var image = System.IO.File.OpenRead(file);
            return File(image, contentType);
        }


        [HttpGet]
        [Route("getBusinessLogoImage")]
        public IActionResult GetBusinessLogoImage(string businessid)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "BusinessLogo");

            // Search for a file that starts with the primary key.
            var file = Directory.GetFiles(imagesPath, $"{businessid}.*").FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            var fileType = Path.GetExtension(file).ToLower();

            // Determine the content type based on the file extension.
            var contentType = fileType switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream",
            };

            var image = System.IO.File.OpenRead(file);
            return File(image, contentType);
        }



        [HttpGet]
        [Route("getBoxImage")]
        public IActionResult GetBoxImage(string boxid)
        {
            var BoxImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "BoxImage");

            // Search for a file that starts with the primary key.
            var file = Directory.GetFiles(BoxImagesPath, $"{boxid}.*").FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            var fileType = Path.GetExtension(file).ToLower();

            // Determine the content type based on the file extension.
            var contentType = fileType switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream",
            };

            var image = System.IO.File.OpenRead(file);
            return File(image, contentType);
        }
    }
}

