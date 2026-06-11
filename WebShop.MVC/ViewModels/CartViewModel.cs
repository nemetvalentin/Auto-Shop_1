using WebShop.DAL.Models;

namespace WebShop.MVC.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
        public int TotalCount => Items.Sum(i => i.Quantity);
    }
}
