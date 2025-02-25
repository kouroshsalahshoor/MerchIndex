using Microsoft.EntityFrameworkCore;
using MerchIndex.Auto.Client.Models;
using MerchIndex.Auto.Data;
using MerchIndex.Auto.Repository.IRepository;

namespace MerchIndex.Auto.Repository
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product)!);
            if (order.Id == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}