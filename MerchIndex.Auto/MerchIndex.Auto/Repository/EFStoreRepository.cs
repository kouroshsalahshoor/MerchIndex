using MerchIndex.Auto.Client.Models;
using MerchIndex.Auto.Data;
using MerchIndex.Auto.Repository.IRepository;

namespace MerchIndex.Auto.Repository
{
    public class EFStoreRepository : IStoreRepository
    {
        private ApplicationDbContext context;
        public EFStoreRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
        public void CreateProduct(Product p)
        {
            context.Add(p);
            context.SaveChanges();
        }
        public void DeleteProduct(Product p)
        {
            context.Remove(p);
            context.SaveChanges();
        }
        public void SaveProduct(Product p)
        {
            context.SaveChanges();
        }
    }
}
