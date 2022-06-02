#nullable disable
using System.ComponentModel.DataAnnotations;
using FoodOrder.Helpers;
using Microsoft.AspNetCore.Mvc;
using FoodOrder.Models;
using Persistence;
using Persistence.Services;

namespace FoodOrder.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFoodOrderService _service;

        public OrdersController(IFoodOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SaveOrderViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            if (cart == default)
            {
                ModelState.AddModelError("","A kosár üres");
                return View(vm);
            }

            var cartItems = "";

            foreach (var item in cart)
            {
                cartItems += item.ItemId;
                cartItems += " ";
                cartItems += item.Quantity;
                cartItems += ",";
            }

            cartItems = cartItems.Remove(cartItems.Length - 1);

            if (!Validator.TryValidateObject(vm, new ValidationContext(vm), null))
            {
                return View(vm);
            }

            var order = new Order()
            {
                Name = vm.Name,
                Address = vm.Address,
                PhoneNumber = vm.PhoneNumber,
                CartItems = cartItems,
                TimeOfOrder = DateTime.Now,
                TimeOfCompletion = null,
                TotalPrice = GetTotalPrice(cartItems)
            };

            if (!_service.SaveOrder(order))
            {
                ModelState.AddModelError("", "Sikertelen rendelés.");
                return View(vm);
            }

            IncreaseOrederedCnt(order);
            cart.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

        private int GetTotalPrice(string cartItems)
        {
            if (cartItems == null)
                return 0;
            if (cartItems.Length == 0)
                return 0;

            var sum = 0;
            var cartItemsSubs = cartItems.Split(',');
            foreach (var item in cartItemsSubs)
            {
                var itemSubs = item.Split(' ');
                sum += int.Parse(itemSubs[1]) * _service.GetItem(int.Parse(itemSubs[0])).Price;
            }

            return sum;
        }

        private void IncreaseOrederedCnt(Order order)
        {
            var cartSubs = order.CartItems.Split(',');
            if (!cartSubs.Any()) return;

            foreach(var item in cartSubs)
            {
                var itemSubs = item.Split(' ');
                _service.IncreaseItemOrderedCnt(_service.GetItem(int.Parse(itemSubs[0])), int.Parse(itemSubs[1]));
            }
        }
        
    }
}
