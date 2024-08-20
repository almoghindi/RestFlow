using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestFlow.DAL.Entities
{
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableId { get; set; }

        [Required]
        [StringLength(50)]
        public string TableNumber { get; set; }

        [Range(1, 20, ErrorMessage = "Table capacity must be between 1 and 20.")]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public bool ChangeAvailability()
        {
            return !IsAvailable;
        }
    }
}
