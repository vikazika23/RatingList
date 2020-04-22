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
    [Route("api/podcast")]
    public class CriticController
    {
        private ILogger<CriticController> Logger { get; }
        private ICriticCreateService CriticCreateService { get; }
        private ICriticGetService CriticGetService { get; }
        private ICriticUpdateService CriticUpdateService { get; }
        private IMapper Mapper { get; }

        public CriticController(ILogger<CriticController> logger, IMapper mapper, ICriticCreateService podcastCreateService, ICriticGetService podcastGetService, ICriticUpdateService podcastUpdateService)
        {
            this.Logger = logger;
            this.CriticCreateService = podcastCreateService;
            this.CriticGetService = podcastGetService;
            this.CriticUpdateService = podcastUpdateService;
            this.Mapper = mapper;
        }

        [HttpPut]
        [Route("")]
        public async Task<CriticDTO> PutAsync(CriticCreateDTO podcast)
        {
            this.Logger.LogTrace($"{nameof(this.PutAsync)} called");

            var result = await this.CriticCreateService.CreateAsync(this.Mapper.Map<CriticUpdateModel>(podcast));

            return this.Mapper.Map<CriticDTO>(result);
        }

        [HttpPatch]
        [Route("")]
        public async Task<CriticDTO> PatchAsync(CriticUpdateDTO podcast)
        {
            this.Logger.LogTrace($"{nameof(this.PutAsync)} called");

            var result = await this.CriticUpdateService.UpdateAsync(this.Mapper.Map<CriticUpdateModel>(podcast));

            return this.Mapper.Map<CriticDTO>(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<CriticDTO>> GetAsync()
        {
            this.Logger.LogTrace($"{nameof(this.GetAsync)} called");

            return this.Mapper.Map<IEnumerable<CriticDTO>>(await this.CriticGetService.GetAsync());
        }

        [HttpGet]
        [Route("{podcastId}")]
        public async Task<CriticDTO> GetAsync(int podcastId)
        {
            this.Logger.LogTrace($"{nameof(this.GetAsync)} called for {podcastId}");

            return this.Mapper.Map<CriticDTO>(await this.CriticGetService.GetAsync(new CriticIdentityModel(podcastId)));
        }
    }
}