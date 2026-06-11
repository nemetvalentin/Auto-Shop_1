using System.Text.Json;
using Microsoft.AspNetCore.Http;
using WebShop.BLL.Interfaces;
using WebShop.DAL.Models;

namespace WebShop.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "ShoppingCart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public List<CartItem> GetCart()
        {
            var json = Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(json))
                return new List<CartItem>();
            return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }

        public void AddToCart(CartItem item)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existing != null)
                existing.Quantity += item.Quantity;
            else
                cart.Add(item);
            SaveCart(cart);
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (quantity <= 0)
                    cart.Remove(item);
                else
                    item.Quantity = quantity;
            }
            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            cart.RemoveAll(c => c.ProductId == productId);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            Session.Remove(CartSessionKey);
        }

        public int GetCartCount()
        {
            return GetCart().Sum(c => c.Quantity);
        }

        public decimal GetCartTotal()
        {
            return GetCart().Sum(c => c.UnitPrice * c.Quantity);
        }
    }
}
