using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestFlow.DAL.Entities
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

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

        public bool IsAvailable { get; set; }

        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();

        public void AddQuantity(decimal quantityToAdd)
        {
            Quantity += quantityToAdd;
            if (Quantity > 0)
            {
                IsAvailable = true;
            }
        }

        public void RemoveQuantity(decimal quantityToRemove)
        {
            Quantity -= quantityToRemove;
            if (Quantity <= 0)
            {
                Quantity = 0;
                IsAvailable = false;
            }
        }
    }
}
