using Microsoft.EntityFrameworkCore;
using WebShop.BLL.Interfaces;
using WebShop.DAL;
using WebShop.DAL.Models;

namespace WebShop.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly AutoShopDbContext _context;

        public OrderService(AutoShopDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(int userId, List<CartItem> cartItems, string? shippingAddress, string? notes)
        {
            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.Now,
                Status = "Pending",
                ShippingAddress = shippingAddress,
                Notes = notes,
                TotalAmount = cartItems.Sum(i => i.UnitPrice * i.Quantity),
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetOrdersByUserAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId, int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;
            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
