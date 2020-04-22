using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.DataAccess.Contracts;
using Media.Domain;
using Media.Domain.Models;

namespace Media.BLL.Implementation
{
    public class CriticUpdateService : ICriticUpdateService
    {
        private ICriticDataAccess CriticDataAccess { get; }
        private IRatingGetService RatingGetService { get; }

        public CriticUpdateService(ICriticDataAccess podcastDataAccess, IRatingGetService ratingGetService)
        {
            CriticDataAccess = podcastDataAccess;
            RatingGetService = ratingGetService;
        }

        public async Task<Critic> UpdateAsync(CriticUpdateModel podcast)
        {
            await RatingGetService.ValidateAsync(podcast);
            return await CriticDataAccess.UpdateAsync(podcast);

        }
    }
}