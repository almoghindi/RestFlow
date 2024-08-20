using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestFlow.DAL.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int OrderId { get; set; }

        [Required]
        [ForeignKey("Table")]
        public int TableId { get; set; }

        public Table Table { get; set; }

        [Required]
        [ForeignKey("Waiter")]
        public int WaiterId { get; set; }

        public Waiter Waiter { get; set; }

        [Required]
        public ICollection<Dish> Dishes { get; set; } = new HashSet<Dish>();

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be a positive value.")]
        public decimal TotalAmount { get; set; }

        public void AddDish(Dish dish)
        {
            Dishes.Add(dish);
            TotalAmount += dish.Price;
        }
        public void CalculateTotalAmount()
        {
            TotalAmount = Dishes.Sum(d => d.Price);
        }
        public bool CloseOrder()
        {
            return IsCompleted = true;
        }

    }

}
