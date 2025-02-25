using MerchIndex.Auto.Client.Models;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;

namespace MerchIndex.Auto.Services
{
    public class ExcelService
    {
        private readonly IWebHostEnvironment _env;
        private const string _rootFolder = "files";

        public ExcelService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task UploadFile(string parentDirName, string dirName, IBrowserFile uploadingFile)
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

            var fileInfo = new FileInfo(uploadingFile.Name);

            var newFileName = $"{Guid.NewGuid()}{fileInfo.Extension}";
            var filePath = Path.Combine(dir, newFileName);

            await using var fs = new FileStream(filePath, FileMode.Create);
            await uploadingFile.OpenReadStream(uploadingFile.Size).CopyToAsync(fs);
        }
        public List<Product> GetProducts(string parentDir, string fileName)
        {
            var items = new List<Product>();
            string path = Path.Combine(_env.WebRootPath, _rootFolder, parentDir, fileName);

            var fileInfo = new FileInfo(path);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(fileInfo);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            //var worksheet = package.Workbook.Worksheets[0];
            if (worksheet != null)
            {
                var nbrOfRows = worksheet.Dimension.End.Row;
                var nbrOfCols = worksheet.Dimension.End.Column;

                for (int RowIndex = 2; RowIndex <= nbrOfRows; RowIndex++)
                {
                    var product = new Product
                    {
                        Name = worksheet.Cells[RowIndex, 1].Value.ToString()!,
                        Price = decimal.Parse(worksheet.Cells[RowIndex, 2].Value.ToString()!),
                        Category = worksheet.Cells[RowIndex, 3].Value.ToString()!,
                        Manifacturer = worksheet.Cells[RowIndex, 4].Value.ToString(),
                        //CompanyId = 
                    };
                    items.Add(product);
                }
            }

            return items;
        }

        public List<Product> GetProducts()
        {
            var items = new List<Product>();
            string path = Path.Combine(_env.WebRootPath, _rootFolder, "merchantdata.xlsx");

            var fileInfo = new FileInfo(path);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(fileInfo);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            //var worksheet = package.Workbook.Worksheets[0];
            if (worksheet != null)
            {
                var nbrOfRows = worksheet.Dimension.End.Row;
                var nbrOfCols = worksheet.Dimension.End.Column;

                for (int RowIndex = 2; RowIndex <= nbrOfRows; RowIndex++)
                {
                    var product = new Product();
                    product.Name = worksheet.Cells[RowIndex, 1].Value.ToString()!;

                    var priceText = worksheet.Cells[RowIndex, 2].Value.ToString()!;
                    string curSep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                    priceText = priceText.Replace(".", curSep);
                    priceText = priceText.Replace(",", curSep);

                    if (decimal.TryParse(priceText, out decimal price))
                    {
                        product.Price = price;
                    }

                    product.Category = worksheet.Cells[RowIndex, 3].Value.ToString()!;
                    product.Manifacturer = worksheet.Cells[RowIndex, 4].Value.ToString();
                    //CompanyId = 

                    items.Add(product);
                }
            }

            return items;
        }
    }
}
