using MerchIndex.Auto.Client.Models;

namespace MerchIndex.Auto.Repository.IRepository
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product p);
        void CreateProduct(Product p);
        void DeleteProduct(Product p);
    }
}
