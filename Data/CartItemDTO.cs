using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CartItemDTO
    {
        public int ItemId { get; set; }
        public ItemDTO Item { get; set; }
        public int Quantity { get; set; }
    }
}
