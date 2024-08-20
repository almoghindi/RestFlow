using System.ComponentModel.DataAnnotations;

namespace RestFlow.API.DTO
{
    public class IngredientDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Quantity must be a positive value.")]
        public decimal Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price per unit must be a positive value.")]
        public decimal PricePerUnit { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

    }
}
