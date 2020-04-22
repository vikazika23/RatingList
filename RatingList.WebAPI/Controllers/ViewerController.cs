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
    [Route("api/track")]
    public class ViewerController
    {
        private ILogger<ViewerController> Logger { get; }
        private IViewerCreateService ViewerCreateService { get; }
        private IViewerGetService ViewerGetService { get; }
        private IViewerUpdateService ViewerUpdateService { get; }
        private IMapper Mapper { get; }

        public ViewerController(ILogger<ViewerController> logger, IMapper mapper, IViewerCreateService trackCreateService, IViewerGetService trackGetService, IViewerUpdateService trackUpdateService)
        {
            this.Logger = logger;
            this.ViewerCreateService = trackCreateService;
            this.ViewerGetService = trackGetService;
            this.ViewerUpdateService = trackUpdateService;
            this.Mapper = mapper;
        }

        [HttpPut]
        [Route("")]
        public async Task<ViewerDTO> PutAsync(ViewerCreateDTO track)
        {
            this.Logger.LogTrace($"{nameof(this.PutAsync)} called");

            var result = await this.ViewerCreateService.CreateAsync(this.Mapper.Map<ViewerUpdateModel>(track));

            return this.Mapper.Map<ViewerDTO>(result);
        }

        [HttpPatch]
        [Route("")]
        public async Task<ViewerDTO> PatchAsync(ViewerUpdateDTO track)
        {
            this.Logger.LogTrace($"{nameof(this.PutAsync)} called");

            var result = await this.ViewerUpdateService.UpdateAsync(this.Mapper.Map<ViewerUpdateModel>(track));

            return this.Mapper.Map<ViewerDTO>(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ViewerDTO>> GetAsync()
        {
            this.Logger.LogTrace($"{nameof(this.GetAsync)} called");

            return this.Mapper.Map<IEnumerable<ViewerDTO>>(await this.ViewerGetService.GetAsync());
        }

        [HttpGet]
        [Route("{trackId}")]
        public async Task<ViewerDTO> GetAsync(int trackId)
        {
            this.Logger.LogTrace($"{nameof(this.GetAsync)} called for {trackId}");

            return this.Mapper.Map<ViewerDTO>(await this.ViewerGetService.GetAsync(new ViewerIdentityModel(trackId)));
        }
    }
}