using System.ComponentModel.DataAnnotations;

namespace RestFlow.API.DTO
{
    public class TableDTO
    {
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        [StringLength(50)]
        public string TableNumber { get; set; }

        [Range(1, 20, ErrorMessage = "Table capacity must be between 1 and 20.")]
        public int Capacity { get; set; }
    }
}
