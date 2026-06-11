using WebShop.DAL.Models;

namespace WebShop.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int userId, List<CartItem> cartItems, string? shippingAddress, string? notes);
        Task<List<Order>> GetOrdersByUserAsync(int userId);
        Task<Order?> GetOrderByIdAsync(int orderId, int userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    }
}
