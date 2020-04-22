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
using Viewer = Media.Domain.Viewer;

namespace Media.DataAccess.Implementations
{
    public class ViewerDataAccess : IViewerDataAccess
    {
        private RatingContext Context { get; }
        private IMapper Mapper { get; }

        public ViewerDataAccess(RatingContext context, IMapper mapper)
        {
            this.Context = context;
            Mapper = mapper;
        }

        public async Task<Viewer> InsertAsync(ViewerUpdateModel viewer)
        {
            var result = await this.Context.AddAsync(this.Mapper.Map<DataAccess.Entities.Viewer>(viewer));
            await this.Context.SaveChangesAsync();
            return this.Mapper.Map<Viewer>(result.Entity);
        }

        public async Task<IEnumerable<Viewer>> GetAsync()
        {
            return this.Mapper.Map<IEnumerable<Viewer>>(
                await this.Context.Viewer.Include(x => x.Rating).ToListAsync());

        }

        public async Task<Viewer> GetAsync(IViewerIdentity viewerId)
        {
            var result = await this.Get(viewerId);
            return this.Mapper.Map<Viewer>(result);
        }

        public async Task<Viewer> UpdateAsync(ViewerUpdateModel viewer)
        {
            var existing = await this.Get(viewer);

            var result = this.Mapper.Map(viewer, existing);

            this.Context.Update(result);

            await this.Context.SaveChangesAsync();

            return this.Mapper.Map<Viewer>(result);
        }

        public async Task<Viewer> GetByAsync(IViewerContainer viewer)
        {
            return viewer.ViewerId.HasValue 
                ? this.Mapper.Map<Viewer>(await this.Context.Viewer.FirstOrDefaultAsync(x => x.Id == viewer.ViewerId)) 
                : null;
        }

        private async Task<Media.DataAccess.Entities.Viewer> Get(IViewerIdentity viewerId)
        {
            if(viewerId == null)
                throw new ArgumentNullException(nameof(viewerId));
            return await this.Context.Viewer.Include(x => x.Rating).FirstOrDefaultAsync(x => x.Id == viewerId.Id);
        }
    }
}