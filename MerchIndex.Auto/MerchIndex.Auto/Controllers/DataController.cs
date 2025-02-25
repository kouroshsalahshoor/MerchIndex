using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MerchIndex.Auto.Client.Models;
using MerchIndex.Auto.Data;

namespace MerchIndex.Auto.Controllers
{
    [ApiController]
    [Route("/api/people")]
    public class DataController : ControllerBase
    {
        private ApplicationDbContext context;
        public DataController(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        [HttpGet]
        public IEnumerable<Person> GetAll()
        {
            IEnumerable<Person> people = context.People.Include(p => p.Department).Include(p => p.Location);
            foreach (Person p in people)
            {
                if (p.Department?.People != null)
                {
                    p.Department.People = null;
                }
                if (p.Location?.People != null)
                {
                    p.Location.People = null;
                }
            }
            return people;
        }
        [HttpGet("{id}")]
        public async Task<Person> GetDetails(long id)
        {
            Person p = await context.People.Include(p => p.Department).Include(p => p.Location).FirstAsync(p => p.Id == id);
            if (p.Department?.People != null)
            {
                p.Department.People = null;
            }
            if (p.Location?.People != null)
            {
                p.Location.People = null;
            }
            return p;
        }
        [HttpPost]
        public async Task Save([FromBody] Person p)
        {
            await context.People.AddAsync(p);
            await context.SaveChangesAsync();
        }
        [HttpPut]
        public async Task Update([FromBody] Person p)
        {
            context.Update(p);
            await context.SaveChangesAsync();
        }
        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            context.People.Remove(new Person() { Id = id });
            await context.SaveChangesAsync();
        }
        [HttpGet("/api/locations")]
        public IAsyncEnumerable<Location> GetLocations() => context.Locations.AsAsyncEnumerable();
        [HttpGet("/api/departments")]
        public IAsyncEnumerable<Department> GetDepts() => context.Departments.AsAsyncEnumerable();
    }
}