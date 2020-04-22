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
    public class CriticGetServiceTests
    {
        [Test]
        public async Task ValidateAsync_CriticExists_DoesNothing()
        {
            // Arrange
            var podcastContainer = new Mock<ICriticContainer>();

            var podcast = new Critic();
            var podcastDataAccess = new Mock<ICriticDataAccess>();
            podcastDataAccess.Setup(x => x.GetByAsync(podcastContainer.Object)).ReturnsAsync(podcast);

            var podcastGetService = new CriticGetService(podcastDataAccess.Object);
            
            // Act
            var action = new Func<Task>(() => podcastGetService.ValidateAsync(podcastContainer.Object));
            
            // Assert
            await action.Should().NotThrowAsync<Exception>();
        }
        
        [Test]
        public async Task ValidateAsync_CriticNotExists_ThrowsError()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Create<int>();
            
            var podcastContainer = new Mock<ICriticContainer>();
            podcastContainer.Setup(x => x.CriticId).Returns(id);

            var podcast = new Critic();
            var podcastDataAccess = new Mock<ICriticDataAccess>();
            podcastDataAccess.Setup(x => x.GetByAsync(podcastContainer.Object)).ReturnsAsync((Critic)null);

            var podcastGetService = new CriticGetService(podcastDataAccess.Object);
            
            // Act
            var action = new Func<Task>(() => podcastGetService.ValidateAsync(podcastContainer.Object));
            
            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>($"Critic not found by id {id}");
        }
    }
}