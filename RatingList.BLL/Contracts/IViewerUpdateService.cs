using System.Threading.Tasks;
using Media.Domain;
using Media.Domain.Models;

namespace Media.BLL.Contracts
{
    public interface IViewerUpdateService
    {
        Task<Viewer> UpdateAsync(ViewerUpdateModel track);
    }
}