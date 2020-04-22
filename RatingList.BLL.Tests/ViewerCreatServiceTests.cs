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
    public class ViewerCreatServiceTests
    {
        public class ScreeningCreateServiceTests
        {
            [Test]
            public async Task CreateAsync_ViewerValidationSucceed_CreatesScreening()
            {
                // Arrange
                var track = new ViewerUpdateModel();
                var expected = new Viewer();
                var ratingGetService = new Mock<IRatingGetService>();
                ratingGetService.Setup(x => x.ValidateAsync(track));
                var trackDataAccess = new Mock<IViewerDataAccess>();
                trackDataAccess.Setup(x => x.InsertAsync(track)).ReturnsAsync(expected);
                var trackGetService = new ViewerCreateService(trackDataAccess.Object, ratingGetService.Object);
                // Act
                var result = await trackGetService.CreateAsync(track);
                // Assert
                result.Should().Be(expected);
            }
            [Test]
            public async Task CreateAsync_ViewerValidationFailed_ThrowsError()
            {
                // Arrange
                var fixture = new Fixture();
                var track = new ViewerUpdateModel();
                var expected = fixture.Create<string>();
                var ratingGetService = new Mock<IRatingGetService>();
                ratingGetService
                    .Setup(x => x.ValidateAsync(track))
                    .Throws(new InvalidOperationException(expected));
                var trackDataAccess = new Mock<IViewerDataAccess>();
                var trackGetService = new ViewerCreateService(trackDataAccess.Object, ratingGetService.Object);
                // Act
                var action = new Func<Task>(() => trackGetService.CreateAsync(track));
                // Assert
                await action.Should().ThrowAsync<InvalidOperationException>().WithMessage(expected);
                trackDataAccess.Verify(x => x.InsertAsync(track), Times.Never);
            }
        }
    }
}