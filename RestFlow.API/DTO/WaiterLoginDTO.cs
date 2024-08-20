using System.ComponentModel.DataAnnotations;

namespace RestFlow.API.DTO
{
    public class WaiterLoginDTO
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
