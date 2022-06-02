using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class CategoryDTO
    {

        public CategoryDTO()
        {
            Items = new HashSet<ItemDTO>();
        }

        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        public virtual ICollection<ItemDTO> Items { get; set; }
    }
}
