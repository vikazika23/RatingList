using System.Collections.Generic;
using System.Threading.Tasks;
using Media.Domain;
using Media.Domain.Contracts;

namespace Media.BLL.Contracts
{
    public interface ICriticGetService
    {
        Task<IEnumerable<Critic>> GetAsync();
        Task<Critic> GetAsync(ICriticIdentity podcast);
        Task ValidateAsync(ICriticContainer departmentContainer);
    }
}