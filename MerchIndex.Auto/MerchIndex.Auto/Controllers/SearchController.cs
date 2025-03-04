using MerchIndex.Auto.Client.Models;
using MerchIndex.Auto.Client.ViewModels;
using MerchIndex.Auto.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MerchIndex.Auto.Controllers
{
    [Route("api/Search")]
    //[Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SearchController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<List<SearchResultVm>> Get(string? filter)
        {
            var result = new List<SearchResultVm>();

            var products = await _db.Products
                .Include(x=> x.Company)
                .Where(
                    x => filter == null || 
                    x.Name.Contains(filter) ||
                    (x.Manifacturer == null || x.Manifacturer.Contains(filter)) ||
                    x.Company.Name.Contains(filter)
                )
                .OrderBy(x => x.Name)
                .Take(5)
                .ToListAsync();

            var companies = await _db.Companies
                .Where(
                    x => filter == null ||
                    x.Name.Contains(filter) ||
                    x.BusinessArea.Contains(filter) ||
                    x.City.Contains(filter) ||
                    x.District.Contains(filter)
                )
                .OrderBy(x => x.Name)
                .Take(5)
                .ToListAsync();

            foreach (var item in products)
            {
                var vm = new SearchResultVm();
                vm.Product = item;
                vm.City = item.Company.City;
                vm.District = item.Company.District;

                item.Company.Products = null;
                result.Add(vm);
            }

            foreach (var item in companies)
            {
                item.Products = null;
                result.Add(new SearchResultVm { Company = item });
            }

            return result;
        } 
    }
}
