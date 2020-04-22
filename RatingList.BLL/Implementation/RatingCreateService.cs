using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.DataAccess.Contracts;
using Media.DataAccess.Implementations;
using Media.Domain;
using Media.Domain.Models;

namespace Media.BLL.Implementation
{
    public class RatingCreateService : IRatingCreateService
    {
        private IRatingDataAccess RatingDataAccess { get; }

        public RatingCreateService(IRatingDataAccess ratingDataAccess)
        {
            RatingDataAccess = ratingDataAccess;
        }

        public Task<Rating> CreateAsync(RatingUpdateModel rating)
        {
            return  RatingDataAccess.InsertAsync(rating);

        }
    }
}