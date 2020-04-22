using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.DataAccess.Contracts;
using Media.Domain;
using Media.Domain.Contracts;

namespace Media.BLL.Implementation
{
    public class ViewerGetService : IViewerGetService
    {
        private IViewerDataAccess ViewerDataAccess { get; }
        
        public ViewerGetService(IViewerDataAccess trackDataAccess)
        {
            this.ViewerDataAccess = trackDataAccess;
        }
        public Task<IEnumerable<Viewer>> GetAsync()
        {
            return this.ViewerDataAccess.GetAsync();
        }

        public Task<Viewer> GetAsync(IViewerIdentity track)
        {
            return this.ViewerDataAccess.GetAsync(track);
        }

        public async Task ValidateAsync(IViewerContainer trackContainer)
        {
            if (trackContainer == null)
            {
                throw new ArgumentNullException(nameof(trackContainer));
            }
            
            var rating = await this.GetBy(trackContainer);

            if (trackContainer.ViewerId.HasValue && rating == null)
            {
                throw new InvalidOperationException($"Rating not found by id {trackContainer.ViewerId}");
            }
        }
        private Task<Viewer> GetBy(IViewerContainer departmentContainer)
        {
            return this.ViewerDataAccess.GetByAsync(departmentContainer);
        }
    }
}