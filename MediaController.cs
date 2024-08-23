using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly string _mediaRootPath = Environment.GetEnvironmentVariable("MEDIA_ROOT_PATH");

        public MediaController()
        {
            if (!Directory.Exists(_mediaRootPath))
            {
                Directory.CreateDirectory(_mediaRootPath);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file == null) return BadRequest("No file uploaded.");

                var filePath = Path.Combine(_mediaRootPath, Guid.NewGuid() + Path.GetExtension(file.FileName));
                
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Ok(new { message = "File uploaded successfully.", path = filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("retrieve/{fileName}")]
        public IActionResult Retrieve(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_mediaRootPath, fileName);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var memoryStream = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    stream.CopyTo(memoryStream);
                }
                memoryStream.Position = 0;

                return File(memoryStream, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{fileName}")]
        public IActionResult Delete(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_mediaRootPath, fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Ok("File deleted successfully.");
                }
                else
                {
                    return NotFound("File not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}