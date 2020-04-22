using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Media.BLL.Contracts;
using Media.DataAccess.Contracts;
using Media.Domain;
using Media.Domain.Contracts;

namespace Media.BLL.Implementation
{
    public class CriticGetService : ICriticGetService
    {
        private ICriticDataAccess CriticDataAccess { get; }
        
        public CriticGetService(ICriticDataAccess podcastDataAccess)
        {
            this.CriticDataAccess = podcastDataAccess;
        }
        public Task<IEnumerable<Critic>> GetAsync()
        {
            return this.CriticDataAccess.GetAsync();
        }

        public Task<Critic> GetAsync(ICriticIdentity podcast)
        {
            return this.CriticDataAccess.GetAsync(podcast);
        }

        public async Task ValidateAsync(ICriticContainer podcastContainer)
        {
            if (podcastContainer == null)
            {
                throw new ArgumentNullException(nameof(podcastContainer));
            }
            
            var rating = await this.GetBy(podcastContainer);

            if (podcastContainer.CriticId.HasValue && rating == null)
            {
                throw new InvalidOperationException($"Rating not found by id {podcastContainer.CriticId}");
            }
        }
        private Task<Critic> GetBy(ICriticContainer departmentContainer)
        {
            return this.CriticDataAccess.GetByAsync(departmentContainer);
        }
    }
}