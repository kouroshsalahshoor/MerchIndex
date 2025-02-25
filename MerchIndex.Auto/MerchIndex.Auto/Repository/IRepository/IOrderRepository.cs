using MerchIndex.Auto.Client.Models;

namespace MerchIndex.Auto.Repository.IRepository
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
