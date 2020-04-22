using System.Collections.Generic;
using System.Threading.Tasks;
using Media.DataAccess.Entities;
using Media.Domain;
using Media.Domain.Contracts;
using Media.Domain.Models;
using Critic = Media.Domain.Critic;

namespace Media.DataAccess.Contracts
{
    public interface ICriticDataAccess
    {
        Task<Critic> InsertAsync(CriticUpdateModel movie);
        Task<IEnumerable<Critic>> GetAsync();
        Task<Critic> GetAsync(ICriticIdentity movieId);
        Task<Critic> UpdateAsync(CriticUpdateModel movie);
        Task<Critic> GetByAsync(ICriticContainer movie);
    }
}