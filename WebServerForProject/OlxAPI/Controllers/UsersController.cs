using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OlxAPI.Data.Entities;
using OlxAPI.Enums;
using OlxAPI.Helpers;
using OlxAPI.Models.PostModels.Parameters;
using OlxAPI.Models.UpdateModels;
using OlxAPI.Models.ViewModels;
using OlxAPI.Services.Models;
using OlxAPI.Services.Services.Abstractions;

namespace OlxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : IdentityControllerBase<User>
    {
        private IMapper _mapper;
        private IAdsService _adsService;
        private readonly UserHelper _userHelper;

        public UsersController(UserManager<User> userManager, IMapper mapper, IAdsService adsService, UserHelper userHelper)
            :base(userManager)
        {
            _mapper = mapper;
            _adsService = adsService;
            _userHelper = userHelper;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<UserViewModel> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var viewModel = _mapper.Map<UserViewModel>(user);
            return viewModel;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<UserViewModel> UpdateUser([FromBody] UserUpdateModel updateModel)
        {
            if(!ModelState.IsValid||updateModel==null)
            {
                throw new BadHttpRequestException("Not valid model");
            }
            var user = await GetIdentityUserAsync();
            if(!string.IsNullOrEmpty(updateModel.Email) && updateModel.Email != user.Email)
            {

                var res = await _userManager.SetEmailAsync(user, updateModel.Email);
                if (!res.Succeeded)
                {
                    throw new BadHttpRequestException(string.Join(Environment.NewLine, res.Errors.Select(e => e.Description)));
                }
            }

            if (!string.IsNullOrEmpty(updateModel.UserName) && updateModel.UserName != user.UserName)
            {
                var res = await _userManager.SetUserNameAsync(user, updateModel.UserName);
            }

            if (!string.IsNullOrEmpty(updateModel.Password)
                &&!string.IsNullOrEmpty(updateModel.OldPassword)
                && await _userManager.CheckPasswordAsync(user, updateModel.OldPassword))
            {
                var res = await _userManager.ChangePasswordAsync(user, updateModel.OldPassword, updateModel.Password);
                if(!res.Succeeded)
                {
                    throw new BadHttpRequestException(string.Join(Environment.NewLine, res.Errors.Select(e => e.Description)));
                }
            }

            return _mapper.Map<UserViewModel>(user);
        }


        [HttpGet]
        [Route("myAds")]
        [Authorize]
        public async Task<object> GetUserAdsAsync([FromQuery] PaginationQueryParameters paginationParams,
            [FromQuery] SortQueryParameters sortParams)
        {
            var user = await GetIdentityUserAsync();
            var paginationModel = _mapper.Map<PaginationParametersModel>(paginationParams);
            var filterModel = new FilterParametersModel { FilterItem = FilterItemEnum.UserId, ItemId =  user.Id};
            var sortModel = _mapper.Map<SortParametersModel>(sortParams);
            var adsModel = await _adsService.GetAsync(paginationModel, filterModel, sortModel);
            var AdViewModel = _mapper.Map<IEnumerable<AdViewModel>>(adsModel);
            var parametersViewModel = _mapper.Map<PaginationParametersModel>(paginationModel);

            return new { Ads = AdViewModel, paginationParameters = parametersViewModel };
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task DeleteAsync(string id)
        {
            var deleteUser = await _userManager.FindByIdAsync(id);
            var isAdmin = await _userHelper.IsAdmin(deleteUser);
            if (!isAdmin)
            {
                var res = await _userManager.DeleteAsync(deleteUser);
            }
            else
            {
                throw new BadHttpRequestException("Forbidden", 403);
            }
        }
    }
}
