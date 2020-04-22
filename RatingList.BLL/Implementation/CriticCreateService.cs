using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.DataAccess.Contracts;
using Media.DataAccess.Implementations;
using Media.Domain;
using Media.Domain.Models;

namespace Media.BLL.Implementation
{
    public class CriticCreateService : ICriticCreateService
    {
        private ICriticDataAccess CriticDataAccess { get; }
        private IRatingGetService RatingGetService { get; }

        public CriticCreateService(ICriticDataAccess podcastDataAccess, IRatingGetService ratingGetService)
        {
            CriticDataAccess = podcastDataAccess;
            RatingGetService = ratingGetService;
        }

        public async Task<Critic> CreateAsync(CriticUpdateModel podcast)
        {
            await RatingGetService.ValidateAsync(podcast);
            return await CriticDataAccess.InsertAsync(podcast);

        }
    }
}