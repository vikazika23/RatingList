using System.Collections.Generic;
using System.Threading.Tasks;
using Media.Domain;
using Media.Domain.Contracts;

namespace Media.BLL.Contracts
{
    public interface IRatingGetService
    {
        Task<IEnumerable<Rating>> GetAsync();
        Task<Rating> GetAsync(IRatingIdentity rating);
        Task ValidateAsync(IRatingContainer departmentContainer);
    }
}