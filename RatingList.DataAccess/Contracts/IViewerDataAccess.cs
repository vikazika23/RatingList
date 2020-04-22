using System.Collections.Generic;
using System.Threading.Tasks;
using Media.DataAccess.Entities;
using Media.Domain;
using Media.Domain.Contracts;
using Media.Domain.Models;
using Viewer = Media.Domain.Viewer;

namespace Media.DataAccess.Contracts
{
    public interface IViewerDataAccess
    {
        Task<Viewer> InsertAsync(ViewerUpdateModel movie);
        Task<IEnumerable<Viewer>> GetAsync();
        Task<Viewer> GetAsync(IViewerIdentity movieId);
        Task<Viewer> UpdateAsync(ViewerUpdateModel movie);
        Task<Viewer> GetByAsync(IViewerContainer movie);
    }
}