using Media.Domain.Contracts;

namespace Media.Domain.Models
{
    public class RatingIdentityModel : IRatingIdentity
    {
        public int Id { get; }

        public RatingIdentityModel(int id)
        {
            this.Id = id;
        }
    }
}