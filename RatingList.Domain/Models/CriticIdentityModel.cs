using Media.Domain.Contracts;

namespace Media.Domain.Models
{
    public class CriticIdentityModel : ICriticIdentity
    {
        public int Id { get; }

        public CriticIdentityModel(int id)
        {
            this.Id = id;
        }
    }
}