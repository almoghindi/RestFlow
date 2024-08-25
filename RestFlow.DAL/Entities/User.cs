using System.ComponentModel.DataAnnotations;

namespace RestFlow.DAL.Entities
{
    public class User
    {
        [Required]
        [MinLength(3)]
        public string? Username { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        [Required]
        public int RestaurantId { get; set; }
    }
}
