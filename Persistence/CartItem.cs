using Data;

namespace Persistence
{
    public class CartItem
    {
        public CartItem()
        {

        }
        public CartItem(int itemId)
        {
            ItemId = itemId;
            Quantity = 1;
        }

        public CartItem(int itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }

        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public CartItemDTO ToDto()
        {
            var cartItemDto = new CartItemDTO()
            {
                ItemId = ItemId, Quantity = Quantity
            };
            return cartItemDto;
        }
    }
}
