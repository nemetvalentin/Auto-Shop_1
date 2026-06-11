using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.BLL.Interfaces;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ShopController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? categoryId, decimal? minPrice, decimal? maxPrice, string? search, string? manufacturer, bool? inStock)
        {
            var products = await _productService.GetFilteredAsync(categoryId, minPrice, maxPrice, search, manufacturer, inStock);
            var categories = await _categoryService.GetAllAsync();
            var manufacturers = await _productService.GetManufacturersAsync();

            var vm = new ShopViewModel
            {
                Products = products,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = c.Id == categoryId
                }).ToList(),
                Manufacturers = manufacturers,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Search = search,
                Manufacturer = manufacturer,
                InStock = inStock
            };

            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(ProductViewModel.FromEntity(product));
        }
    }
}
