using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestFlow.API.Controllers;
using RestFlow.API.DTO;
using RestFlow.BL.Services;
using RestFlow.DAL.Entities;

namespace RestFlow.Tests
{
    public class CategoryTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoryController _controller;

        public CategoryTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _controller = new CategoryController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnOkResult_WithCategories()
        {
            var categories = new List<CategoryDto>
            {
                new CategoryDto { CategoryId = 1, Name = "Category1", Description = "Description1" },
                new CategoryDto { CategoryId = 2, Name = "Category2", Description = "Description2" }
            };

            _mockCategoryService.Setup(service => service.GetAll())
    .ReturnsAsync(new List<Category>
    {
        new Category { CategoryId = 1, Name = "Category1", Description = "Description1" },
        new Category { CategoryId = 2, Name = "Category2", Description = "Description2" }
    }.AsEnumerable());

            var result = await _controller.GetCategories() as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public async Task AddCategory_ShouldReturnCreatedAtActionResult()
        {
            var categoryDto = new CategoryDto { CategoryId = 1, Name = "New Category", Description = "New Description" };

            var result = await _controller.AddCategory(categoryDto) as CreatedAtActionResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(201);
            result.ActionName.Should().Be("GetCategories");
            result.RouteValues["id"].Should().Be(categoryDto.CategoryId);
            result.Value.Should().BeEquivalentTo(categoryDto);
        }

        [Fact]
        public async Task DeleteCategory_ShouldReturnNotFound_WhenCategoryDoesNotExist()
        {
            int categoryId = 3; 

            _mockCategoryService.Setup(service => service.GetAll())
                .ReturnsAsync(new List<Category>
                {
            new Category { CategoryId = 1, Name = "Category1", Description = "Description1" },
            new Category { CategoryId = 2, Name = "Category2", Description = "Description2" }
                }.AsEnumerable());

            var result = await _controller.DeleteCategory(categoryId) as NotFoundObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
            result.Value.Should().Be("Category not found.");
        }

        [Fact]
        public async Task UpdateCategory_ShouldReturnNoContent_WhenCategoryUpdatedSuccessfully()
        {
            var category = new Category { CategoryId = 1, Name = "Updated Category", Description = "Updated Description" };

            _mockCategoryService.Setup(service => service.GetAll())
    .ReturnsAsync(new List<Category>
    {
        new Category { CategoryId = 1, Name = "Category1", Description = "Description1" },
        new Category { CategoryId = 2, Name = "Category2", Description = "Description2" }
    }.AsEnumerable());

            var result = await _controller.UpdateCategory(category.CategoryId, category) as NoContentResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task UpdateCategory_ShouldReturnBadRequest_WhenIdMismatch()
        {
            var category = new Category { CategoryId = 2, Name = "Updated Category", Description = "Updated Description" };

            var result = await _controller.UpdateCategory(1, category) as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Category ID mismatch.");
        }
    }
}
