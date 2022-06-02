namespace Data
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string CartItems { get; set; } = null!; // "item1.id,item1.quantity,item2.id,item2.quantity,..."
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime TimeOfOrder { get; set; }
        public DateTime? TimeOfCompletion { get; set; }
        public int TotalPrice { get; set; }
    }
}
