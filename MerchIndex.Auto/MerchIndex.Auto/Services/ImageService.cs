using Microsoft.AspNetCore.Components.Forms;
using System;

namespace MerchIndex.Auto.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _env;
        private const string _rootFolder = "images";

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string GetUrl(string parentDirName, string dirName)
        {
            var dir = Path.Combine(_env.WebRootPath, _rootFolder, parentDirName, dirName);

            if (Directory.Exists(dir))
            {
                var files = Directory.GetFiles(dir);
                if (files is not null && files.FirstOrDefault() is not null)
                {
                    var fileName = new FileInfo(files.FirstOrDefault()!).Name;
                    return $"images/{parentDirName}/{dirName}/{fileName}";
                }
            }
            return $"images/default.png";
        }

        public List<FileInfo> GetFileInfos(string parentDirName, string dirName)
        {
            List<FileInfo> fileInfos = new();

            var dir = Path.Combine(_env.WebRootPath, _rootFolder, parentDirName, dirName);
            if (Directory.Exists(dir))
            {
                var files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    fileInfos.Add(new FileInfo(file));
                }
            }

            return fileInfos;
        }

        public async Task UploadFiles(string parentDirName, string dirName, List<IBrowserFile> uploadingFiles)
        {
            var rootDir = Path.Combine(_env.WebRootPath, _rootFolder);
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

            foreach (var file in uploadingFiles)
            {
                var fileInfo = new FileInfo(file.Name);

                var newFileName = $"{Guid.NewGuid()}{fileInfo.Extension}";
                var filePath = Path.Combine(dir, newFileName);

                await using var fs = new FileStream(filePath, FileMode.Create);
                await file.OpenReadStream(file.Size).CopyToAsync(fs);
            }
        }

        public bool Delete(string parentDirName, string dirName, FileInfo fileInfo)
        {
            var file = Path.Combine(_env.WebRootPath, _rootFolder, parentDirName, dirName, fileInfo.Name);
            if (File.Exists(file))
            {
                File.Delete(file);
                return true;
            }
            return false;
        }

        public bool DeleteAll(string parentDirName, string dirName)
        {
            var dir = Path.Combine(_env.WebRootPath, _rootFolder, parentDirName, dirName);
            if (Directory.Exists(dir))
            {
                var dirInfo = new DirectoryInfo(dir);

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
                return true;
            }
            return false;
        }

        public async Task<string> ToBase64(IBrowserFile file)
        {
            using var stream = file.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var base64 = Convert.ToBase64String(fileBytes);
            return $"data:{file.ContentType};base64,{base64}";
        }

        //public async Task<string?> ToBase64(IBrowserFile iBrowserFile)
        //{
        //    var file = iBrowserFile;
        //    var buffer = new byte[file.Size];
        //    if (buffer.Length > 0)
        //    {
        //        await file.OpenReadStream().ReadAsync(buffer);
        //        return $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";
        //    }
        //    return null;
        //}
    }
}
