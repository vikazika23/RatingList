using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Media.DataAccess.Context;
using Media.DataAccess.Contracts;
using Media.DataAccess.Entities;
using Media.Domain.Contracts;
using Media.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Critic = Media.Domain.Critic;

namespace Media.DataAccess.Implementations
{
    public class CriticDataAccess : ICriticDataAccess
    {
        private RatingContext Context { get; }
        private IMapper Mapper { get; }

        public CriticDataAccess(RatingContext context, IMapper mapper)
        {
            this.Context = context;
            Mapper = mapper;
        }

        public async Task<Critic> InsertAsync(CriticUpdateModel critic)
        {
            var result = await this.Context.AddAsync(this.Mapper.Map<DataAccess.Entities.Critic>(critic));
            await this.Context.SaveChangesAsync();
            return this.Mapper.Map<Critic>(result.Entity);
        }

        public async Task<IEnumerable<Critic>> GetAsync()
        {
            return this.Mapper.Map<IEnumerable<Critic>>(
                await this.Context.Critic.Include(x => x.Rating).ToListAsync());

        }

        public async Task<Critic> GetAsync(ICriticIdentity criticId)
        {
            var result = await this.Get(criticId);
            return this.Mapper.Map<Critic>(result);
        }

        public async Task<Critic> UpdateAsync(CriticUpdateModel critic)
        {
            var existing = await this.Get(critic);

            var result = this.Mapper.Map(critic, existing);

            this.Context.Update(result);

            await this.Context.SaveChangesAsync();

            return this.Mapper.Map<Critic>(result);
        }

        public async Task<Critic> GetByAsync(ICriticContainer critic)
        {
            return critic.CriticId.HasValue 
                ? this.Mapper.Map<Critic>(await this.Context.Critic.FirstOrDefaultAsync(x => x.Id == critic.CriticId)) 
                : null;
        }

        private async Task<Media.DataAccess.Entities.Critic> Get(ICriticIdentity criticId)
        {
            if(criticId == null)
                throw new ArgumentNullException(nameof(criticId));
            return await this.Context.Critic.Include(x => x.Rating).FirstOrDefaultAsync(x => x.Id == criticId.Id);
        }
    }
}