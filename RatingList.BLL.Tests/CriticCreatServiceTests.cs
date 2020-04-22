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
    public class CriticCreatServiceTests
    {
        public class ScreeningCreateServiceTests
        {
            [Test]
            public async Task CreateAsync_CriticValidationSucceed_CreatesScreening()
            {
                // Arrange
                var podcast = new CriticUpdateModel();
                var expected = new Critic();
                var ratingGetService = new Mock<IRatingGetService>();
                ratingGetService.Setup(x => x.ValidateAsync(podcast));
                var podcastDataAccess = new Mock<ICriticDataAccess>();
                podcastDataAccess.Setup(x => x.InsertAsync(podcast)).ReturnsAsync(expected);
                var podcastGetService = new CriticCreateService(podcastDataAccess.Object, ratingGetService.Object);
                // Act
                var result = await podcastGetService.CreateAsync(podcast);
                // Assert
                result.Should().Be(expected);
            }
            [Test]
            public async Task CreateAsync_CriticValidationFailed_ThrowsError()
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
                var podcastGetService = new CriticCreateService(podcastDataAccess.Object, ratingGetService.Object);
                // Act
                var action = new Func<Task>(() => podcastGetService.CreateAsync(podcast));
                // Assert
                await action.Should().ThrowAsync<InvalidOperationException>().WithMessage(expected);
                podcastDataAccess.Verify(x => x.InsertAsync(podcast), Times.Never);
            }
        }
    }
}