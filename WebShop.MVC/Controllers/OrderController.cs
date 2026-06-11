using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.BLL.Interfaces;
using WebShop.DAL.Models;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Istorija porudžbina za prijavljenog korisnika
        public async Task<IActionResult> MyOrders()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var orders = await _orderService.GetOrdersByUserAsync(userId);
            var vm = orders.Select(OrderViewModel.FromEntity).ToList();
            return View(vm);
        }

        // Detalji porudžbine
        public async Task<IActionResult> Details(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            // Admins can see any order
            Order? order;
            if (User.IsInRole("Admin"))
            {
                var allOrders = await _orderService.GetAllOrdersAsync();
                order = allOrders.FirstOrDefault(o => o.Id == id);
            }
            else
            {
                order = await _orderService.GetOrderByIdAsync(id, userId);
            }

            if (order == null) return NotFound();

            return View(OrderViewModel.FromEntity(order));
        }

        // Admin: sve porudžbine
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var vm = orders.Select(OrderViewModel.FromEntity).ToList();
            return View(vm);
        }

        // Admin: promena statusa
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            TempData["Success"] = "Status porudžbine je ažuriran.";
            return RedirectToAction("AllOrders");
        }
    }
}
