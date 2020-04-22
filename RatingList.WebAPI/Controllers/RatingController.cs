using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Media.BLL.Contracts;
using Media.Client.DTO.Read;
using Media.Client.Requests.Create;
using Media.Client.Requests.Update;
using Media.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Media.WebAPI.Controllers
{
    [ApiController]
    [Route("api/rating")]
    public class RatingController
    {
        private ILogger<RatingController> Logger { get; }
        private IRatingCreateService RatingCreateService { get; }
        private IRatingGetService RatingGetService { get; }
        private IRatingUpdateService RatingUpdateService { get; }
        private IMapper Mapper { get; }

        public RatingController(ILogger<RatingController> logger, IMapper mapper, IRatingCreateService ratingCreateService, IRatingGetService ratingGetService, IRatingUpdateService ratingUpdateService)
        {
            this.Logger = logger;
            this.RatingCreateService = ratingCreateService;
            this.RatingGetService = ratingGetService;
            this.RatingUpdateService = ratingUpdateService;
            this.Mapper = mapper;
        }

        [HttpPut]
        [Route("")]
        public async Task<RatingDTO> PutAsync(RatingCreateDTO rating)
        {
            this.Logger.LogTrace($"{nameof(this.PutAsync)} called");

            var result = await this.RatingCreateService.CreateAsync(this.Mapper.Map<RatingUpdateModel>(rating));

            return this.Mapper.Map<RatingDTO>(result);
        }

        [HttpPatch]
        [Route("")]
        public async Task<RatingDTO> PatchAsync(RatingUpdateDTO rating)
        {
            this.Logger.LogTrace($"{nameof(this.PutAsync)} called");

            var result = await this.RatingUpdateService.UpdateAsync(this.Mapper.Map<RatingUpdateModel>(rating));

            return this.Mapper.Map<RatingDTO>(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<RatingDTO>> GetAsync()
        {
            this.Logger.LogTrace($"{nameof(this.GetAsync)} called");

            return this.Mapper.Map<IEnumerable<RatingDTO>>(await this.RatingGetService.GetAsync());
        }

        [HttpGet]
        [Route("{ratingId}")]
        public async Task<RatingDTO> GetAsync(int ratingId)
        {
            this.Logger.LogTrace($"{nameof(this.GetAsync)} called for {ratingId}");

            return this.Mapper.Map<RatingDTO>(await this.RatingGetService.GetAsync(new RatingIdentityModel(ratingId)));
        }
    }
}