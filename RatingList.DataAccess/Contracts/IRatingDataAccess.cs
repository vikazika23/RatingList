using System.Collections.Generic;
using System.Threading.Tasks;
using Media.DataAccess.Entities;
using Media.Domain;
using Media.Domain.Contracts;
using Media.Domain.Models;
using Rating = Media.Domain.Rating;

namespace Media.DataAccess.Contracts
{
    public interface IRatingDataAccess
    {
        Task<Rating> InsertAsync(RatingUpdateModel rating);
        Task<IEnumerable<Rating>> GetAsync();
        Task<Rating> GetAsync(IRatingIdentity ratingId);
        Task<Rating> UpdateAsync(RatingUpdateModel rating);
        Task<Rating> GetByAsync(IRatingContainer departmentId);
    }
}