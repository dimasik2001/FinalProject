using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OlxAPI.Data.Entities;
using OlxAPI.Enums;
using OlxAPI.Models.PostModels.Parameters;
using OlxAPI.Models.ViewModels;
using OlxAPI.Services.Models;
using OlxAPI.Services.Services.Abstractions;

namespace OlxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<User> _userManager;
        private IMapper _mapper;
        private IAdsService _adsService;

        public UsersController(UserManager<User> userManager, IMapper mapper, IAdsService adsService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _adsService = adsService;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<UserViewModel> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var viewModel = _mapper.Map<UserViewModel>(user);
            return viewModel;
        }

        [HttpGet]
        [Route("{id}/ads")]
        public async Task<object> GetAsync([FromQuery] PaginationQueryParameters paginationParams,
            [FromQuery] SortQueryParameters sortParams, string id)
        {
            var paginationModel = _mapper.Map<PaginationParametersModel>(paginationParams);
            var filterModel = new FilterParametersModel { FilterItem = FilterItemEnum.UserId, ItemId =  id};
            var sortModel = _mapper.Map<SortParametersModel>(sortParams);
            var adModel = await _adsService.GetAsync(paginationModel, filterModel, sortModel);
            var AdViewModel = _mapper.Map<IEnumerable<AdViewModel>>(adModel);
            var parametersViewModel = _mapper.Map<PaginationParametersModel>(paginationModel);

            return new { Ad = AdViewModel, paginationParameters = parametersViewModel };
        }
    }
}
