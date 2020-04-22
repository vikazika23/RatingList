using System.Threading.Tasks;
using Media.Domain;
using Media.Domain.Models;

namespace Media.BLL.Contracts
{
    public interface IViewerCreateService
    {
        Task<Viewer> CreateAsync(ViewerUpdateModel track);
    }
}