using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OlxAPI.Data.Entities;
using OlxAPI.Helpers;
using OlxAPI.Models;
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
    public class AdsController : IdentityControllerBase<User>
    {
        private readonly IAdsService _adsService;
        private readonly IMapper _mapper;
        private readonly UserHelper _userHelper;
        private readonly IImagesService _imagesService;
        private readonly IWebHostEnvironment _appEnvironment;

        public AdsController(IAdsService adsService,
            IMapper mapper,
            UserManager<User> userManager,
            UserHelper userHelper,
            IWebHostEnvironment appEnvironment, IImagesService imagesService)
            : base(userManager)
        {
            _appEnvironment = appEnvironment;
            _adsService = adsService;
            _mapper = mapper;
            _userHelper = userHelper;
            _imagesService = imagesService;
        }

        [HttpGet]
        public async Task<object> GetAsync([FromQuery] PaginationQueryParameters paginationParams,
            [FromQuery] FilterQueryParameters filterParams,
            [FromQuery] SortQueryParameters sortParams)
        {
            var paginationModel = _mapper.Map<PaginationParametersModel>(paginationParams);
            var filterModel = _mapper.Map<FilterParametersModel>(filterParams);
            var sortModel = _mapper.Map<SortParametersModel>(sortParams);
            var adModel = await _adsService.GetAsync(paginationModel, filterModel, sortModel);
            var AdViewModel = _mapper.Map<IEnumerable<AdViewModel>>(adModel);
            var parametersViewModel = _mapper.Map<PaginationParametersModel>(paginationModel);

            return new { Ads = AdViewModel, paginationParameters = parametersViewModel };
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<AdViewModel> GetByIdAsync(int id)
        {
            var model = await _adsService.GetAsync(id);
            return _mapper.Map<AdViewModel>(model);
        }

        [HttpPost]
        [Authorize()]
        public async Task<AdViewModel> CreateAsync([FromBody] AdPostModel postModel)
        {
            var model = _mapper.Map<AdModel>(postModel);
            var user = await GetIdentityUserAsync();
            model.UserId = user.Id;
            var newModel = await _adsService.CreateAsync(model);
            return _mapper.Map<AdViewModel>(newModel);
        }

        [HttpDelete]
        [Route("{adId}")]
        [Authorize()]
        public async Task DeleteAsync(int adId)
        {
            var user = await GetIdentityUserAsync();
            var ad = await _adsService.GetAsync(adId);
            var isOwner = await _userHelper.IsOwner(user.Id, adId);
            var isAdmin = await _userHelper.IsAdmin(user);
            if (isOwner || isAdmin)
            {
                foreach (var item in ad.Images)
                {
                    _imagesService.DeleteFile(Path.Combine(_appEnvironment.WebRootPath, item));
                }
                
                await _adsService.DeleteAsync(adId);
            }
        }

        [Route("{id}")]
        [HttpPut]
        [Authorize()]
        public async Task<AdViewModel> UpdateAsync([FromBody] AdPostModel postModel, int id)
        {

            var user = await GetIdentityUserAsync();
            var isOwner = await _userHelper.IsOwner(user.Id, id);
            if (isOwner)
            {
                var model = _mapper.Map<AdModel>(postModel);
                model.Id = id;
                model.UserId = user.Id;
                var newModel = await _adsService.UpdateAsync(model);
                return _mapper.Map<AdViewModel>(newModel);
            }
            throw new BadHttpRequestException("Forbidden", 403);

        }

        [Route("categories")]
        [HttpGet]
        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            var models = await _adsService.GetCategories();
            var viewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(models);
            return viewModels;
        }
    }
}
