using System.Collections.Generic;
using System.Threading.Tasks;
using Media.Domain;
using Media.Domain.Contracts;

namespace Media.BLL.Contracts
{
    public interface IViewerGetService
    {
        Task<IEnumerable<Viewer>> GetAsync();
        Task<Viewer> GetAsync(IViewerIdentity track);
        Task ValidateAsync(IViewerContainer departmentContainer);
    }
}