using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.DataAccess.Contracts;
using Media.Domain;
using Media.Domain.Models;

namespace Media.BLL.Implementation
{
    public class RatingUpdateService : IRatingUpdateService
    {
        private IRatingDataAccess RatingDataAccess { get; }

        public RatingUpdateService(IRatingDataAccess ratingDataAccess)
        {
            ratingDataAccess = RatingDataAccess;
        }

        public Task<Rating> UpdateAsync(RatingUpdateModel rating)
        {
            return RatingDataAccess.UpdateAsync(rating);
        }
    }
}