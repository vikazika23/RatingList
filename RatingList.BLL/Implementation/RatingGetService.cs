using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.DataAccess.Contracts;
using Media.DataAccess.Entities;
using Media.Domain.Contracts;
using Rating = Media.Domain.Rating;

namespace Media.BLL.Implementation
{
    public class RatingGetService : IRatingGetService
    {
        private IRatingDataAccess RatingDataAccess { get; }
        
        public RatingGetService(IRatingDataAccess ratingDataAccess)
        {
            this.RatingDataAccess = ratingDataAccess;
        }
        public Task<IEnumerable<Rating>> GetAsync()
        {
            return this.RatingDataAccess.GetAsync();
        }
        
        public Task<Rating> GetAsync(IRatingIdentity rating)
        {
            return this.RatingDataAccess.GetAsync(rating);
        }

        public async Task ValidateAsync(IRatingContainer ratingContainer)
        {
            if (ratingContainer == null)
            {
                throw new ArgumentNullException(nameof(ratingContainer));
            }
            
            var rating = await this.GetBy(ratingContainer);

            if (ratingContainer.RatingId.HasValue && rating == null)
            {
                throw new InvalidOperationException($"Rating not found by id {ratingContainer.RatingId}");
            }
        }
        private Task<Rating> GetBy(IRatingContainer ratingContainer)
        {
            return this.RatingDataAccess.GetByAsync(ratingContainer);
        }
    }
}