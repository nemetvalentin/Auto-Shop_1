using WebShop.DAL.Models;

namespace WebShop.BLL.Interfaces
{
    public interface ICartService
    {
        List<CartItem> GetCart();
        void AddToCart(CartItem item);
        void UpdateQuantity(int productId, int quantity);
        void RemoveFromCart(int productId);
        void ClearCart();
        int GetCartCount();
        decimal GetCartTotal();
    }
}
