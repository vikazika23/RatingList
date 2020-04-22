using System;
using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.BLL.Implementation;
using Media.DataAccess.Contracts;
using Media.Domain;
using Media.Domain.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
namespace Media.BLL.Tests
{
    public class CriticUpdateServiceTest
    {
        [Test]
        public async Task UpdateAsync_CriticValidationSucceed_CreatesScreening()
        {
            // Arrange
            var podcast = new CriticUpdateModel();
            var expected = new Critic();
            
            var ratingGetService = new Mock<IRatingGetService>();
            ratingGetService.Setup(x => x.ValidateAsync(podcast));
            
            var podcastDataAccess = new Mock<ICriticDataAccess>();
            podcastDataAccess.Setup(x => x.UpdateAsync(podcast)).ReturnsAsync(expected);
            
            var podcastGetService = new CriticUpdateService(podcastDataAccess.Object, ratingGetService.Object);
            
            // Act
            var result = await podcastGetService.UpdateAsync(podcast);
            
            // Assert
            result.Should().Be(expected);
        }
        
        [Test]
        public async Task UpdateAsync_CriticValidationFailed_ThrowsError()
        {
            // Arrange
            var fixture = new Fixture();
            var podcast = new CriticUpdateModel();
            var expected = fixture.Create<string>();
            
            var ratingGetService = new Mock<IRatingGetService>();
            ratingGetService
                .Setup(x => x.ValidateAsync(podcast))
                .Throws(new InvalidOperationException(expected));
            
            var podcastDataAccess = new Mock<ICriticDataAccess>();
            var podcastGetService = new CriticUpdateService(podcastDataAccess.Object, ratingGetService.Object);
            
            // Act
            var action = new Func<Task>(() => podcastGetService.UpdateAsync(podcast));
            
            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage(expected);
            podcastDataAccess.Verify(x => x.UpdateAsync(podcast), Times.Never);
        }
    }
}