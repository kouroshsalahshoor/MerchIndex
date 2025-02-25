using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MerchIndex.Auto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpPost("save")]
        public async Task<IActionResult> SaveImage([FromBody] ImageDataModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.ImageData))
            {
                return BadRequest();
            }

            var base64Data = Regex.Match(model.ImageData, @"data:image/(?<type>.+?);base64,(?<data>.+)").Groups["data"].Value;
            var imageBytes = Convert.FromBase64String(base64Data);

            var filePath = Path.Combine("wwwroot", "images", "camera", $"{Guid.NewGuid()}.png");
            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            return Ok(new { Path = filePath });
        }

        public class ImageDataModel
        {
            public string ImageData { get; set; }
        }
    }
}
