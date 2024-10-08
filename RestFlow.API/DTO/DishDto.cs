﻿using System.ComponentModel.DataAnnotations;

namespace RestFlow.API.DTO
{
    public class DishDto
    {

        [Key]
        public int DishId { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool IsAvailable { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public List<int> ingredientIds { get; set; } = new List<int>();
    }

}
