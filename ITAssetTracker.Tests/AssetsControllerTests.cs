using Microsoft.EntityFrameworkCore;
using ITAssetTracker.Controllers;
using ITAssetTracker.Data;
using ITAssetTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ITAssetTracker.Tests
{
    public class AssetsControllerTests
    {
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsViewWithAllAssets()
        {
            // Arrange
            var context = GetInMemoryContext();
            context.Assets.Add(new Asset { Name = "Test Laptop", Type = "Laptop", Status = "Available" });
            context.Assets.Add(new Asset { Name = "Test Monitor", Type = "Monitor", Status = "Available" });
            await context.SaveChangesAsync();
            var controller = new AssetsController(context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Asset>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var context = GetInMemoryContext();
            var controller = new AssetsController(context);

            // Act
            var result = await controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_WithValidAsset_AddsToDatabase()
        {
            // Arrange
            var context = GetInMemoryContext();
            var controller = new AssetsController(context);
            var newAsset = new Asset { Name = "New Printer", Type = "Printer", Status = "Available" };

            // Act
            await controller.Create(newAsset);

            // Assert
            Assert.Equal(1, await context.Assets.CountAsync());
        }
    }
}