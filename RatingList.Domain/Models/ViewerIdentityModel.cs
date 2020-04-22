using Media.Domain.Contracts;

namespace Media.Domain.Models
{
    public class ViewerIdentityModel : IViewerIdentity
    {
        public int Id { get; }

        public ViewerIdentityModel(int id)
        {
            this.Id = id;
        }
    }
}