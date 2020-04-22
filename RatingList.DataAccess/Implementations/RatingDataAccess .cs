using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Media.DataAccess.Context;
using Media.DataAccess.Contracts;
using Media.Domain;
using Media.Domain.Contracts;
using Media.Domain.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Media.DataAccess.Entities;
using Rating = Media.Domain.Rating;

namespace Media.DataAccess.Implementations
{
    public class RatingDataAccess : IRatingDataAccess
    {
        private RatingContext Context { get; }
        private IMapper Mapper { get; }

        public RatingDataAccess(RatingContext context, IMapper mapper)
        {
            this.Context = context;
            Mapper = mapper;
        }

        public async Task<Rating> InsertAsync(RatingUpdateModel rating)
        {
            var result = await this.Context.AddAsync(this.Mapper.Map<DataAccess.Entities.Rating>(rating));
            await this.Context.SaveChangesAsync();
            return this.Mapper.Map<Rating>(result.Entity);
        }

        public async Task<IEnumerable<Rating>> GetAsync()
        {
            return this.Mapper.Map<IEnumerable<Rating>>(
                await this.Context.Rating.ToListAsync());

        }

        public async Task<Rating> GetAsync(IRatingIdentity rating)
        {
            var result = await this.Get(rating);
            return this.Mapper.Map<Rating>(result);
        }

        public async Task<Rating> UpdateAsync(RatingUpdateModel rating)
        {
            var existing = await this.Get(rating);

            var result = this.Mapper.Map(rating, existing);

            this.Context.Update(result);

            await this.Context.SaveChangesAsync();

            return this.Mapper.Map<Rating>(result);
        }

        public async Task<Rating> GetByAsync(IRatingContainer rating)
        {
            return rating.RatingId.HasValue 
                ? this.Mapper.Map<Rating>(await this.Context.Rating.FirstOrDefaultAsync(x => x.Id == rating.RatingId)) 
                : null;
        }

        private async Task<Media.DataAccess.Entities.Rating> Get(IRatingIdentity rating)
        {
            if(rating == null)
                throw new ArgumentNullException(nameof(rating));
            return await this.Context.Rating.FirstOrDefaultAsync(x => x.Id == rating.Id);
        }
    }
}