using System;
using System.Threading.Tasks;
using AutoFixture;
using Media.BLL.Implementation;
using Media.DataAccess.Contracts;
using Media.Domain;
using Media.Domain.Contracts;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Media.BLL.Tests
{
    public class RatingGetServiceTests
    {
        [Test]
        public async Task ValidateAsync_RatingExists_DoesNothing()
        {
            // Arrange
            var ratingContainer = new Mock<IRatingContainer>();

            var rating = new Rating();
            var ratingDataAccess = new Mock<IRatingDataAccess>();
            ratingDataAccess.Setup(x => x.GetByAsync(ratingContainer.Object)).ReturnsAsync(rating);

            var ratingGetService = new RatingGetService(ratingDataAccess.Object);
            
            // Act
            var action = new Func<Task>(() => ratingGetService.ValidateAsync(ratingContainer.Object));
            
            // Assert
            await action.Should().NotThrowAsync<Exception>();
        }
        
        [Test]
        public async Task ValidateAsync_RatingNotExists_ThrowsError()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Create<int>();
            
            var ratingContainer = new Mock<IRatingContainer>();
            ratingContainer.Setup(x => x.RatingId).Returns(id);

            var rating = new Rating();
            var ratingDataAccess = new Mock<IRatingDataAccess>();
            ratingDataAccess.Setup(x => x.GetByAsync(ratingContainer.Object)).ReturnsAsync((Rating)null);

            var ratingGetService = new RatingGetService(ratingDataAccess.Object);
            
            // Act
            var action = new Func<Task>(() => ratingGetService.ValidateAsync(ratingContainer.Object));
            
            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>($"Rating not found by id {id}");
        }
    }
}