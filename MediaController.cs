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
            try
            {
                if (!Directory.Exists(_mediaRootPath))
                {
                    Directory.CreateDirectory(_mediaRootPath);
                }
            }
            catch (Exception ex)
            {
                // Assuming you have a logging mechanism
                Console.WriteLine($"Failed to create directory {_mediaRootPath}: {ex}");
                // Consider how you want to handle initialization failures - crash, or allow to run with degraded functionality?
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null) return BadRequest("No file uploaded.");

            var filePath = Path.Combine(_mediaRootPath, Guid.NewGuid() + Path.GetExtension(file.FileName));

            try
            {
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Ok(new { message = "File uploaded successfully.", path = filePath });
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO Exception encountered while uploading file: {ex.Message}");
                return StatusCode(500, "Error occurred while uploading the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during file upload: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("retrieve/{fileName}")]
        public IActionResult Retrieve(string fileName)
        {
            var filePath = Path.Combine(_mediaRootPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            try
            {
                var memoryStream = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    stream.CopyTo(memoryStream);
                }
                memoryStream.Position = 0;

                return File(memoryStream, "application/octet-stream", fileName);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO Exception encountered while retrieving file: {ex.Message}");
                return StatusCode(500, "Error occurred while retrieving the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during file retrieval: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{fileName}")]
        public IActionResult Delete(string fileName)
        {
            var filePath = Path.Combine(_mediaRootPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            try
            {
                System.IO.File.Delete(filePath);
                return Ok("File deleted successfully.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO Exception encountered while deleting file: {ex.Message}");
                return StatusCode(500, "Error occurred while deleting the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during file deletion: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}