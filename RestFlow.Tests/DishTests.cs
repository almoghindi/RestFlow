﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestFlow.API.Controllers;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

namespace RestFlow.Tests
{
    public class DishControllerTests
    {
        private readonly Mock<IDishService> _mockDishService;
        private readonly DishController _controller;

        public DishControllerTests()
        {
            _mockDishService = new Mock<IDishService>();
            _controller = new DishController(_mockDishService.Object);
        }

        [Fact]
        public async Task UpdateDish_ShouldReturnNoContent_WhenDishIsUpdated()
        {
            var dishDto = new DishDto
            {
                DishId = 1,
                Name = "Updated Dish",
                Price = 20.0m,
                CategoryId = 1,
                IsAvailable = true,
                ImageUrl = "Updated Description",
                RestaurantId = 1,
            };

            var dish = new Dish
            {
                DishId = dishDto.DishId,
                Name = dishDto.Name,
                Price = dishDto.Price,
                CategoryId = dishDto.CategoryId,
                IsAvailable = dishDto.IsAvailable,
                ImageUrl = dishDto.ImageUrl,
                RestaurantId = dishDto.RestaurantId,
            };

            _mockDishService.Setup(service => service.GetById(dishDto.DishId))
                .ReturnsAsync(dish);
            _mockDishService.Setup(service => service.Update(dishDto.DishId,dishDto.Name, dishDto.Price, dishDto.CategoryId, dishDto.IsAvailable, dishDto.ingredientIds, dishDto.ImageUrl,dishDto.RestaurantId))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateDish(dishDto.DishId, dishDto) as NoContentResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task UpdateDish_ShouldReturnBadRequest_WhenIdMismatch()
        {

            var dish = new DishDto
            {
                DishId = 2,
                Name = "Updated Dish",
                Price = 20.0m,
                CategoryId = 2,
                IsAvailable = true,
                ImageUrl = "Updated Description"
            };

            var result = await _controller.UpdateDish(1, dish) as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Dish ID mismatch.");
        }

        [Fact]
        public async Task AddDish_ShouldReturnOkResult_WhenDishIsAdded()
        {
            var dishDto = new DishDto
            {
                DishId = 1,
                Name = "New Dish",
                Price = 15.0m,
                CategoryId = 1,
                IsAvailable = true,
                ingredientIds = new List<int> { 1 },
                ImageUrl = "New Description",
                RestaurantId = 1
            };

            _mockDishService.Setup(service => service.Add(
                dishDto.Name,
                dishDto.Price,
                dishDto.CategoryId,
                dishDto.IsAvailable,
                dishDto.ingredientIds,
                dishDto.ImageUrl,
                dishDto.RestaurantId))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddDish(dishDto) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(dishDto);
        }
        [Fact]
        public async Task GetDishes_ShouldReturnOkResult_WhenDishesExist()
        {
            var dishes = new List<Dish>
    {
        new Dish { DishId = 1, Name = "Dish 1", Price = 10.0m, CategoryId = 1, IsAvailable = true, ImageUrl = "Description 1", RestaurantId = 1 },
        new Dish { DishId = 2, Name = "Dish 2", Price = 20.0m, CategoryId = 2, IsAvailable = false, ImageUrl = "Description 2", RestaurantId = 1 }
    };

            _mockDishService.Setup(service => service.GetAllByRestaurantId(1))
                .ReturnsAsync(dishes);

            var result = await _controller.GetDishes(1) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(dishes);
        }

        [Fact]
        public async Task DeleteDish_ShouldReturnNoContent_WhenDishIsDeleted()
        {
            var dishId = 1;
            var dish = new Dish
            {
                DishId = dishId,
                Name = "Dish to be deleted",
                Price = 10.0m,
                CategoryId = 1,
                IsAvailable = true,
                ImageUrl = "Description",
                RestaurantId = 1
            };

            _mockDishService.Setup(service => service.GetById(dishId))
                .ReturnsAsync(dish);
            _mockDishService.Setup(service => service.Delete(dishId))
                .Returns(Task.CompletedTask);

            var result = await _controller.DeleteDish(dishId) as NoContentResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }
    }
}
