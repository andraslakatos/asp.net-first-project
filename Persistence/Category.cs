using System.ComponentModel.DataAnnotations;
using Data;

namespace Persistence
{
    public class Category
    {

        public Category()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }

        public CategoryDTO ToDto()
        {
            var categoryDto = new CategoryDTO
                {Name = Name, Id = Id};
            return categoryDto;
        }
    }
}
