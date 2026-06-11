using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.DAL.Models;

namespace WebShop.MVC.ViewModels
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();
        public List<string> Manufacturers { get; set; } = new();

        // Filter values
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Search { get; set; }
        public string? Manufacturer { get; set; }
        public bool? InStock { get; set; }
    }
}
