using Data;

namespace Persistence
{
    public class Order
    {
        public int Id { get; set; }
        public string CartItems { get; set; } = null!; // "item1.id,item1.quantity,item2.id,item2.quantity,..."
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime TimeOfOrder { get; set; }
        public DateTime? TimeOfCompletion { get; set; }
        public int TotalPrice { get; set; }

        public OrderDTO ToDto()
        {
            var orderDto = new OrderDTO()
            {
                Id = Id, CartItems = CartItems, Name = Name, Address = Address, PhoneNumber = PhoneNumber,
                TimeOfOrder = TimeOfOrder, TimeOfCompletion = TimeOfCompletion, TotalPrice = TotalPrice,
            };
            return orderDto;
        }
    }
}
