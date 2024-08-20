using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestFlow.API.Controllers;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

namespace RestFlow.Tests
{
    public class IngredientTests
    {
        private readonly IngredientController _controller;
        private readonly Mock<IIngredientService> _mockIngredientService;

        public IngredientTests()
        {
            _mockIngredientService = new Mock<IIngredientService>();
            _controller = new IngredientController(_mockIngredientService.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkResult_WhenIngredientExists()
        {
            var ingredientId = 1;
            var ingredient = new Ingredient
            {
                IngredientId = ingredientId,
                Name = "Sugar",
                Quantity = 10,
                PricePerUnit = 2.0m,
                Description = "White sugar"
            };

            _mockIngredientService
                .Setup(service => service.GetById(ingredientId))
                .ReturnsAsync(ingredient);

            var result = await _controller.Get(ingredientId) as ActionResult<Ingredient>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(ingredient);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenIngredientDoesNotExist()
        {
            var ingredientId = 1;

            _mockIngredientService
                .Setup(service => service.GetById(ingredientId))
                .ReturnsAsync((Ingredient)null);

            var result = await _controller.Get(ingredientId) as ActionResult<Ingredient>;

            result.Result.Should().BeOfType<NotFoundResult>();
            var notFoundResult = result.Result as NotFoundResult;
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WhenIngredientsExist()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient { IngredientId = 1, Name = "Sugar", Quantity = 10, PricePerUnit = 2.0m, Description = "White sugar" },
                new Ingredient { IngredientId = 2, Name = "Salt", Quantity = 5, PricePerUnit = 1.0m, Description = "Sea salt" }
            };

            _mockIngredientService
                .Setup(service => service.GetAll())
                .ReturnsAsync(ingredients);

            var result = await _controller.GetAll() as ActionResult<IEnumerable<Ingredient>>;

            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(ingredients);
        }

        [Fact]
        public async Task Create_ShouldReturnOkResult_WhenIngredientIsCreated()
        {
            var ingredientDto = new IngredientDTO
            {
                Name = "Flour",
                Quantity = 20,
                PricePerUnit = 1.5m,
                Description = "All-purpose flour"
            };

            var ingredient = new Ingredient
            {
                IngredientId = 1,
                Name = ingredientDto.Name,
                Quantity = ingredientDto.Quantity,
                PricePerUnit = ingredientDto.PricePerUnit,
                Description = ingredientDto.Description
            };

            _mockIngredientService
    .Setup(service => service.Add(
        ingredientDto.Name,
        ingredientDto.Quantity,
        ingredientDto.PricePerUnit,
        ingredientDto.Description))
    .Returns(Task.CompletedTask);

            var result = await _controller.Create(ingredientDto) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(ingredientDto);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContent_WhenIngredientIsUpdated()
        {
            var ingredientId = 1;
            var ingredient = new Ingredient
            {
                IngredientId = ingredientId,
                Name = "Updated Flour",
                Quantity = 30,
                PricePerUnit = 1.8m,
                Description = "Updated description"
            };

            _mockIngredientService
                .Setup(service => service.Update(ingredient))
                .Returns(Task.CompletedTask);

            var result = await _controller.Update(ingredientId, ingredient) as NoContentResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
        {
            var ingredientId = 1;
            var ingredient = new Ingredient
            {
                IngredientId = 2,
                Name = "Updated Flour",
                Quantity = 30,
                PricePerUnit = 1.8m,
                Description = "Updated description"
            };

            var result = await _controller.Update(ingredientId, ingredient) as BadRequestResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenIngredientIsDeleted()
        {
            var ingredientId = 1;

            _mockIngredientService
                .Setup(service => service.Delete(ingredientId))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(ingredientId) as NoContentResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task AddQuantity_ShouldReturnNoContent_WhenQuantityIsAdded()
        {
            var ingredientId = 1;
            var quantityToAdd = 5.0m;

            _mockIngredientService
                .Setup(service => service.AddQuantity(ingredientId, quantityToAdd))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddQuantity(ingredientId, quantityToAdd) as NoContentResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task RemoveQuantity_ShouldReturnNoContent_WhenQuantityIsRemoved()
        {
            var ingredientId = 1;
            var quantityToRemove = 3.0m;

            _mockIngredientService
                .Setup(service => service.RemoveQuantity(ingredientId, quantityToRemove))
                .Returns(Task.CompletedTask);

            var result = await _controller.RemoveQuantity(ingredientId, quantityToRemove) as NoContentResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }
    }
}
