using FoodOrder.Helpers;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Services;


namespace FoodOrder.Controllers
{
    public class CartController : Controller
    {
        private readonly IFoodOrderService _service;

        public CartController(IFoodOrderService service)
        {
            _service = service;
        }
        
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart == null)
            {
                ViewBag.cartValue = 0;
            }
            else
            {
                ViewBag.cartValue = cart.Sum(i => _service.GetItem(i.ItemId).Price * i.Quantity);
            }
            
            return View();
        }

        public IActionResult AddToCart(int? itemId)
        {
            if (itemId == null) return NotFound();

            var item = _service.GetItem((int)itemId);
            if (item == null) return NotFound();

            if (CartValue((int)itemId) > 20000)
                return View("AddToCartFail");


            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<CartItem>
                {
                    new CartItem((int)itemId)
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart.ToList());
            }
            else
            {
                var cart = (List<CartItem>) SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                var index = TryToGetCartItem((int)itemId);

                if (index == -1)
                {
                    cart.Add(new CartItem((int)itemId));
                }
                else
                {
                    cart[index].Quantity++;
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            //return RedirectToAction("Details", "Home", new {item.Category.Id});
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int itemId)
        {
            var cart = (List<CartItem>) SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart == null) return NotFound();

            int index = TryToGetCartItem(itemId);
            if (index == -1) return NotFound();

            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            cart.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int TryToGetCartItem(int itemId) // return -1 if item not found, else returns index of cart item
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ItemId == itemId) return i;
            }

            return -1;
        }

        public IActionResult AddToCartFail()
        {
            return View("AddToCartFail");
        }

        private int CartValue(int itemId)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var value = 0;
            if (cart != null)
            {
                foreach (var item in cart)
                    value += _service.GetItem(item.ItemId).Price * item.Quantity;
            }

            return value + _service.GetItem(itemId).Price;
        }
    }
}