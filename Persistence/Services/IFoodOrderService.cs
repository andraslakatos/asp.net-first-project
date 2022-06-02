using Data;

namespace Persistence.Services
{
    public interface IFoodOrderService
    {
        public List<Category> GetCategories(string? name = null);
        public Category? GetCategoryById(int id);
        public List<Item> GetItemsByCategoryId(int id);
        public Item? GetItem(int id);
        public List<Item> GetItems();
        public List<Order> GetOrders();
        public Order? GetOrder(int id);
        public bool UpdateOrder(Order order);
        public bool SaveOrder(Order order);
        public List<Item> GetFavorites();
        public bool IncreaseItemOrderedCnt(Item item, int amount);
        public Item? CreateItem(Item item);
        public List<CartItem>? GetCart(int orderId);

    }
}
