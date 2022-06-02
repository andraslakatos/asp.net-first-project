using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class ItemDTO
    {
        public ItemDTO() {}

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
        
        public CategoryDTO Category { get; set; }
    }
}
