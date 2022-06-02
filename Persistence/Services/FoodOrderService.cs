using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class FoodOrderService : IFoodOrderService
    {
        private readonly FoodOrderContext _context;

        public FoodOrderService(FoodOrderContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories(string? name = null)
        {
            return _context.Categories.Where(l => l.Name.Contains(name ?? "")).OrderBy(l => l.Name).ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(l => l.Id == id);
        }

        public List<Item> GetItemsByCategoryId(int id)
        {
            return _context.Items.Where(i => i.Category.Id == id).ToList();
        }

        public Item? GetItem(int id)
        {
            return _context.Items.FirstOrDefault(i => i.Id == id);
        }

        public List<Item> GetItems()
        {
            return _context.Items.ToList();
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public Order? GetOrder(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }
        public bool UpdateOrder(Order order)
        {
            try
            {
                foreach (var item in _context.Orders)
                {
                    if (item.Id == order.Id)
                    {
                        item.Name = order.Name;
                        item.Address = order.Address;
                        item.PhoneNumber = order.PhoneNumber;
                        item.TimeOfCompletion = order.TimeOfCompletion;
                        item.TimeOfOrder = order.TimeOfOrder;
                        item.TotalPrice = order.TotalPrice;
                        item.CartItems = order.CartItems;
                    }
                }

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public bool SaveOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<Item> GetFavorites()
        {
            return _context.Items.OrderByDescending(i => i.OrderedCnt).Take(10).ToList();
        }

        public bool IncreaseItemOrderedCnt(Item item, int amount)
        {
            try
            {
                var currentAmount = _context.Entry(item).Property(i => i.OrderedCnt).CurrentValue;
                _context.Entry(item).Property(i => i.OrderedCnt).CurrentValue = currentAmount + amount;

                _context.Update(item);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Item? CreateItem(Item item)
        {
            try
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return null;
            }

            return item;
        }

        public List<CartItem>? GetCart(int orderId)
        {
            try
            {
                var order = GetOrder(orderId);
                if (order == null) return null;

                var cart = order.CartItems.Split(',');
                var result = new List<CartItem>();
                foreach (var item in cart)
                {
                    var itemSubs = item.Split(' ');
                    var cartItem = new CartItem(int.Parse(itemSubs[0]), int.Parse(itemSubs[1]));
                    result.Add(cartItem);
                }
                return result;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }

}
