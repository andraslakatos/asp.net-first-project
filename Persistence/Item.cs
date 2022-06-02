using System.ComponentModel.DataAnnotations;
using Data;

namespace Persistence
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)] 
        public string Name { get; set; } = null!;

        public int Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        public bool Spicy { get; set; }
        public bool Vegan { get; set; }


        public int OrderedCnt { get; set; }
        public virtual Category Category { get; set; } = null!;

        public ItemDTO ToDto()
        {
            var itemDto = new ItemDTO()
            {
                Id = Id, Name = Name, Price = Price, Description = Description, Spicy = Spicy, Vegan = Vegan,
                OrderedCnt = OrderedCnt, Category = Category.ToDto()
            };
            return itemDto;
        }

    }
}
