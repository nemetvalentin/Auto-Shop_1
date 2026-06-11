using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.BLL.Interfaces;
using WebShop.DAL.Models;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IProductService productService, IOrderService orderService)
        {
            _cartService = cartService;
            _productService = productService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var vm = new CartViewModel
            {
                Items = _cartService.GetCart()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
                return NotFound();

            var item = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductCode = product.Code,
                UnitPrice = product.Price,
                Quantity = quantity,
                ImagePath = product.ImagePath
            };

            _cartService.AddToCart(item);
            TempData["Success"] = $"'{product.Name}' je dodat u korpu.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            _cartService.UpdateQuantity(productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            _cartService.RemoveFromCart(productId);
            TempData["Success"] = "Proizvod je uklonjen iz korpe.";
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var items = _cartService.GetCart();
            if (!items.Any())
                return RedirectToAction("Index");

            var vm = new CheckoutViewModel
            {
                CartItems = items
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmOrder(CheckoutViewModel vm)
        {
            var items = _cartService.GetCart();
            if (!items.Any())
                return RedirectToAction("Index");

            vm.CartItems = items;

            if (!ModelState.IsValid)
                return View("Checkout", vm);

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var order = await _orderService.CreateOrderAsync(userId, items, vm.ShippingAddress, vm.Notes);
            _cartService.ClearCart();

            TempData["Success"] = $"Porudžbina #{order.Id} je uspešno kreirana!";
            return RedirectToAction("OrderConfirmation", new { id = order.Id });
        }

        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var order = await _orderService.GetOrderByIdAsync(id, userId);
            if (order == null) return NotFound();

            return View(OrderViewModel.FromEntity(order));
        }
    }
}
