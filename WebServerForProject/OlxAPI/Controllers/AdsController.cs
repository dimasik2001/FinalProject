using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OlxAPI.Models.PostModels;
using OlxAPI.Models.ViewModels;
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
        public async Task<IEnumerable<AdViewModel>> GetAsync()
        {
            var model = await _adsService.GetAsync();
            return _mapper.Map<IEnumerable<AdViewModel>>(model);
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
            model = await _adsService.CreateAsync(model);
            return _mapper.Map<AdViewModel>(model);
        }
        [Route("{id}")]
        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            await _adsService.DeleteAsync(id);
        }
        [Route("{id}")]
        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<AdViewModel> UpdateAsync([FromBody]AdPostModel postModel, int id)
        {
            var model = _mapper.Map<AdModel>(postModel);
            model.Id = id;
            model.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            model = await _adsService.UpdateAsync(model);
            return _mapper.Map<AdViewModel>(model);
        }

    }
}
