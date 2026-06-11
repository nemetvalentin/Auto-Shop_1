using System.ComponentModel.DataAnnotations;
using WebShop.DAL.Models;

namespace WebShop.MVC.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Adresa dostave je obavezna")]
        [StringLength(500)]
        [Display(Name = "Adresa dostave")]
        public string ShippingAddress { get; set; } = "";

        [StringLength(1000)]
        [Display(Name = "Napomena")]
        public string? Notes { get; set; }

        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalAmount => CartItems.Sum(i => i.TotalPrice);
    }

    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Notes { get; set; }
        public string? Username { get; set; }
        public List<OrderItemViewModel> Items { get; set; } = new();

        public static OrderViewModel FromEntity(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                Notes = order.Notes,
                Username = order.User?.Username,
                Items = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "N/A",
                    ProductCode = oi.Product?.Code ?? "N/A",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                }).ToList()
            };
        }
    }

    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string ProductCode { get; set; } = "";
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
