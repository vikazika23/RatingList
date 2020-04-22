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
    public class ViewerGetServiceTests
    {
        [Test]
        public async Task ValidateAsync_ViewerExists_DoesNothing()
        {
            // Arrange
            var trackContainer = new Mock<IViewerContainer>();

            var track = new Viewer();
            var trackDataAccess = new Mock<IViewerDataAccess>();
            trackDataAccess.Setup(x => x.GetByAsync(trackContainer.Object)).ReturnsAsync(track);

            var trackGetService = new ViewerGetService(trackDataAccess.Object);
            
            // Act
            var action = new Func<Task>(() => trackGetService.ValidateAsync(trackContainer.Object));
            
            // Assert
            await action.Should().NotThrowAsync<Exception>();
        }
        
        [Test]
        public async Task ValidateAsync_ViewerNotExists_ThrowsError()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Create<int>();
            
            var trackContainer = new Mock<IViewerContainer>();
            trackContainer.Setup(x => x.ViewerId).Returns(id);

            var track = new Viewer();
            var trackDataAccess = new Mock<IViewerDataAccess>();
            trackDataAccess.Setup(x => x.GetByAsync(trackContainer.Object)).ReturnsAsync((Viewer)null);

            var trackGetService = new ViewerGetService(trackDataAccess.Object);
            
            // Act
            var action = new Func<Task>(() => trackGetService.ValidateAsync(trackContainer.Object));
            
            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>($"Viewer not found by id {id}");
        }
    }
}