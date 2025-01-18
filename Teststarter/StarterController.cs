using System.Collections.Generic;
using Xunit;
using starter.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Teststarter
{
    public class StarterControllerTests
    {
        private readonly StarterController _controller;

        public StarterControllerTests()
        {
            _controller = new StarterController();
        }

        [Fact]
        public void Get_ReturnsAllItems()
        {
            // Arrange
            ClearItems();
            _controller.Post("item1");
            _controller.Post("item2");

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<List<string>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void Get_WithValidId_ReturnsItem()
        {
            // Arrange
            ClearItems();
            _controller.Post("item1");

            // Act
            var result = _controller.Get(0);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<string>(okResult.Value);
            Assert.Equal("item1", item);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            ClearItems();

            // Act
            var result = _controller.Get(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_AddsItem_ReturnsOk()
        {
            // Arrange
            ClearItems();

            // Act
            var result = _controller.Post("new item");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<List<string>>(okResult.Value);
            Assert.Contains("new item", items);
        }

        [Fact]
        public void Put_WithValidId_UpdatesItem()
        {
            // Arrange
            ClearItems();
            _controller.Post("item1");

            // Act
            var result = _controller.Put(0, "updated item");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<List<string>>(okResult.Value);
            Assert.Equal("updated item", items[0]);
        }

        [Fact]
        public void Put_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            ClearItems();

            // Act
            var result = _controller.Put(999, "updated item");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_WithValidId_RemovesItem()
        {
            // Arrange
            ClearItems();
            _controller.Post("item1");

            // Act
            var result = _controller.Delete(0);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var items = Assert.IsAssignableFrom<List<string>>(okResult.Value);
            Assert.Empty(items);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            ClearItems();

            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private void ClearItems()
        {
            // Clear the static items list
            typeof(StarterController)
                .GetField("items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .SetValue(null, new List<string>());
        }
    }
}
