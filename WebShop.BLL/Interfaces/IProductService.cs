using WebShop.DAL.Models;

namespace WebShop.BLL.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetFilteredAsync(int? categoryId, decimal? minPrice, decimal? maxPrice, string? search, string? manufacturer, bool? inStock = null);
        Task<Product?> GetByIdAsync(int? id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<List<Category>> GetCategoriesWithProductsAsync();
        Task<List<string>> GetManufacturersAsync();
    }
}
