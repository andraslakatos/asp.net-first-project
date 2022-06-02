using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Models
{
    public class SaveOrderViewModel
    {
        [Display(Name = "Név: ")]
        [MaxLength(90), MinLength(3)]
        [RegularExpression(@"^([A-ZÁÉÍÓÖŐÚÜŰ])([a-záéíóöőúüű])*( ([A-ZÁÉÍÓÖŐÚÜŰ])([a-záéíóöőúüű])*)+$")]
        public string Name { get; set; } = null!;

        [Display(Name = "Cím: ")]
        [RegularExpression(@"^[A-ZÁÉÍÓÖŐÚÜŰa-záéíóöőúüű0-9/. \-]*$")]
        public string Address { get; set; } = null!;
        [Display(Name = "Telefonszám: ")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
