using Microsoft.EntityFrameworkCore;
using WebShop.BLL.Interfaces;
using WebShop.DAL;
using WebShop.DAL.Models;

namespace WebShop.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly AutoShopDbContext _context;
        public ProductService(AutoShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<List<Product>> GetFilteredAsync(int? categoryId, decimal? minPrice, decimal? maxPrice, string? search, string? manufacturer, bool? inStock = null)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search) || (p.Description != null && p.Description.Contains(search)) || p.Code.Contains(search));

            if (!string.IsNullOrWhiteSpace(manufacturer))
                query = query.Where(p => p.Name.Contains(manufacturer) || (p.Description != null && p.Description.Contains(manufacturer)));

            if (inStock.HasValue)
                query = query.Where(p => p.InStock == inStock.Value);

            return await query.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int? id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetCategoriesWithProductsAsync()
        {
            return await _context.Categories
            .Include(c => c.Products)
            .ToListAsync();
        }

        public async Task<List<string>> GetManufacturersAsync()
        {
            // Extract known manufacturers from product names/descriptions
            var knownManufacturers = new List<string> { "Intel", "AMD", "NVIDIA", "Samsung", "Kingston", "Asus", "MSI", "Gigabyte", "Corsair", "Logitech" };
            return knownManufacturers;
        }
    }
}
