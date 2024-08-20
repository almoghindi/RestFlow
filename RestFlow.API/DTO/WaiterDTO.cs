using System.ComponentModel.DataAnnotations;

namespace RestFlow.API.DTO
{
    public class WaiterDTO
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(250)]
        public string ContactInformation { get; set; }
    }
}
