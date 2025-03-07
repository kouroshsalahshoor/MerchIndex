using Microsoft.AspNetCore.Mvc;

namespace MerchIndex.Auto.Controllers
{
    [ApiController]
    [Route("/api/uploadimage")]
    public class UploadImageController : ControllerBase
    {
        //https://www.youtube.com/watch?v=tESVLxcD6Bw
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private ApplicationDbContext context;
        public UploadImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("{id}")]
        public void Upload(long? id, [FromBody] string imageData)
        {
            var dir = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            var productDir = Path.Combine(dir, id.ToString()!);
            if (Directory.Exists(productDir) == false)
            {
                Directory.CreateDirectory(productDir);
            }

            var fileName = $"{Guid.NewGuid()}.png";
            var filePath = Path.Combine(productDir, fileName);

            //var fileInfo = new FileInfo(file.Name);

            //var newFileName = $"{Guid.NewGuid()}{fileInfo.Extension}";
            //var newFilePath = Path.Combine(productDir, newFileName);

            //await using var fs = new FileStream(newFilePath, FileMode.Create);
            //await file.OpenReadStream(file.Size).CopyToAsync(fs);

            using var fs = new FileStream(filePath, FileMode.Create);
            using var bw = new BinaryWriter(fs);
            var data = Convert.FromBase64String(imageData.Split(",")[1]);
            bw.Write(data);
            bw.Close();
            fs.Close();

            //create/update model
            //save in db

            //return Json(new {success = true, imagePath = filePath});
        }
    }
}