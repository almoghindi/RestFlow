using System.ComponentModel.DataAnnotations;

namespace RestFlow.API.DTO
{
    public class RestaurantDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }
    }
}
