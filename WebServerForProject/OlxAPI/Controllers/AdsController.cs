using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OlxAPI.Models.DeleteModels;
using OlxAPI.Models.PostModels;
using OlxAPI.Models.PostModels.Parameters;
using OlxAPI.Models.ViewModels;
using OlxAPI.Models.ViewModels.Parameters;
using OlxAPI.Services.Models;
using OlxAPI.Services.Services.Abstractions;

namespace OlxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private IAdsService _adsService;
        private IMapper _mapper;

        public AdsController(IAdsService adsService, IMapper mapper)
        {
            _adsService = adsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<object> GetAsync([FromQuery]PaginationQueryParameters paginationParams,
            [FromQuery] FilterQueryParameters filterParams,
            [FromQuery] SortQueryParameters sortParams)
        {
            var paginationModel = _mapper.Map<PaginationParametersModel>(paginationParams);
            var filterModel = _mapper.Map<FilterParametersModel>(filterParams);
            var sortModel = _mapper.Map<SortParametersModel>(sortParams);
            var adModel = await _adsService.GetAsync(paginationModel, filterModel, sortModel);
            var AdViewModel = _mapper.Map<IEnumerable<AdViewModel>>(adModel);
            var parametersViewModel = _mapper.Map<PaginationParametersModel>(paginationModel);

            return new { Ad = AdViewModel, paginationParameters = parametersViewModel };
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<AdViewModel> GetByIdAsync(int id)
        {
            var model = await _adsService.GetAsync(id);
            return _mapper.Map<AdViewModel>(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<AdViewModel> CreateAsync([FromBody] AdPostModel postModel)
        {
            var model = _mapper.Map<AdModel>(postModel);
            model.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var newModel = await _adsService.CreateAsync(model);
            return _mapper.Map<AdViewModel>(newModel);
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task DeleteAsync([FromBody]AdDeleteModel deleteModel)
        {
            var model = _mapper.Map<AdModel>(deleteModel);
            model.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            await _adsService.DeleteAsync(model);
        }

        [Route("{id}")]
        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<AdViewModel> UpdateAsync([FromBody]AdPostModel postModel, int id)
        {
            var model = _mapper.Map<AdModel>(postModel);
            model.Id = id;
            model.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var newModel = await _adsService.UpdateAsync(model);
            return _mapper.Map<AdViewModel>(newModel);
        }

    }
}
