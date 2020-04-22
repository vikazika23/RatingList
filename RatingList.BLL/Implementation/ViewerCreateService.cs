using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.DataAccess.Contracts;
using Media.DataAccess.Implementations;
using Media.Domain;
using Media.Domain.Models;

namespace Media.BLL.Implementation
{
    public class ViewerCreateService : IViewerCreateService
    {
        private IViewerDataAccess ViewerDataAccess { get; }
        private IRatingGetService RatingGetService { get; }

        public ViewerCreateService(IViewerDataAccess trackDataAccess, IRatingGetService ratingGetService)
        {
            ViewerDataAccess = trackDataAccess;
            RatingGetService = ratingGetService;
        }

        public async Task<Viewer> CreateAsync(ViewerUpdateModel track)
        {
            await RatingGetService.ValidateAsync(track);
            return await ViewerDataAccess.InsertAsync(track);

        }
    }
}