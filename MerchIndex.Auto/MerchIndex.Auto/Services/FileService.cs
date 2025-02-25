using MerchIndex.Auto.Client.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace MerchIndex.Auto.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _env;
        //private const string _rootFolder = "files";

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task UploadFile(string rootFolder, string parentDirName, string dirName, IBrowserFile uploadingFile)
        {
            var rootDir = Path.Combine(_env.WebRootPath, rootFolder);
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }

            var parentDir = Path.Combine(rootDir, parentDirName);
            if (!Directory.Exists(parentDir))
            {
                Directory.CreateDirectory(parentDir);
            }

            var dir = Path.Combine(parentDir, dirName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var fileInfo = new FileInfo(uploadingFile.Name);

            var newFileName = $"{Guid.NewGuid()}{fileInfo.Extension}";
            var filePath = Path.Combine(dir, newFileName);

            await using var fs = new FileStream(filePath, FileMode.Create);
            await uploadingFile.OpenReadStream(uploadingFile.Size).CopyToAsync(fs);
        }
        public void CreateDirIfNotExist(string dir)
        {
            var absolutPath = Path.Combine(_env.WebRootPath, dir);
            if (!Directory.Exists(absolutPath))
            {
                Directory.CreateDirectory(absolutPath);
            }
        }
    }
}
