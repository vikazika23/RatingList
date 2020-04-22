using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.DataAccess.Contracts;
using Media.Domain;
using Media.Domain.Models;

namespace Media.BLL.Implementation
{
    public class ViewerUpdateService : IViewerUpdateService
    {
        private IViewerDataAccess ViewerDataAccess { get; }
        private IRatingGetService RatingGetService { get; }

        public ViewerUpdateService(IViewerDataAccess trackDataAccess, IRatingGetService ratingGetService)
        {
            ViewerDataAccess = trackDataAccess;
            RatingGetService = ratingGetService;
        }

        public async Task<Viewer> UpdateAsync(ViewerUpdateModel track)
        {
          await RatingGetService.ValidateAsync(track);
          return await ViewerDataAccess.UpdateAsync(track);

        }
    }
}