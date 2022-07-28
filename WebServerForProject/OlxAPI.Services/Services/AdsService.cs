using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OlxAPI.Data;
using OlxAPI.Data.Entities;
using OlxAPI.Data.Parameters;
using OlxAPI.Data.Repositories.Abstractions;
using OlxAPI.Services.Models;
using OlxAPI.Services.Services.Abstractions;

namespace OlxAPI.Services.Services
{
    public class AdsService : IAdsService
    {
        private IAdsRepository _repository;
        private IMapper _mapper;

        public AdsService(IAdsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdModel>> GetAsync(PaginationParametersModel paginationModel,
            FilterParametersModel filterModel,
            SortParametersModel sortModel)
        {
            if (paginationModel.Page == null || paginationModel.PageSize == null)
            {
                SetDefaultPaginationParametrs(paginationModel);
            }

            var pagination = _mapper.Map<PaginationParameters>(paginationModel);
            var filter = _mapper.Map<FilterParameters>(filterModel);
            var sort = _mapper.Map<SortParameters>(sortModel);

            var entities = await _repository.GetAsync(pagination, filter, sort);
            _mapper.Map(pagination, paginationModel);

            return _mapper.Map<IEnumerable<AdModel>>(entities);
        }

        public async Task<AdModel> GetAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return _mapper.Map<AdModel>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity.Id);
        }
        public async Task<AdModel> UpdateAsync(AdModel model)
        {

            var entity = _mapper.Map<Ad>(model);
            await _repository.UpdateAsync(entity);

            var updatedEntity = await _repository.GetByIdAsync(entity.Id);
            model = _mapper.Map<AdModel>(updatedEntity);

            return model;
        }

        public async Task<AdModel> CreateAsync(AdModel model)
        {
            var entity = _mapper.Map<Ad>(model);
            await _repository.CreateAsync(entity);

            var newEntity = await _repository.GetByIdAsync(entity.Id);
            model = _mapper.Map<AdModel>(newEntity);

            return model;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            var entities = await _repository.GetCategories();
            var models = _mapper.Map<IEnumerable<CategoryModel>>(entities);
            return models;
        }
        public void SetDefaultPaginationParametrs(PaginationParametersModel parametersModel)
        {
            parametersModel.Page = parametersModel.Page ?? 1;
            parametersModel.PageSize = parametersModel.PageSize ?? 6;
        }
    }
}
