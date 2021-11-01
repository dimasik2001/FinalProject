using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OlxAPI.Data;
using OlxAPI.Data.Entities;
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

        public async Task<IEnumerable<AdModel>> GetAsync()
        {
            var entities = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<AdModel>>(entities);
        }

        public async Task<AdModel> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            return _mapper.Map<AdModel>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task<AdModel> UpdateAsync(AdModel model)
        {
            var entity = _mapper.Map<Ad>(model);
            entity = await _repository.UpdateAsync(entity);
            model = _mapper.Map<AdModel>(entity);
            return model;
        }

        public async Task<AdModel> CreateAsync(AdModel model)
        {
            var entity = _mapper.Map<Ad>(model);
            entity = await _repository.CreateAsync(entity);
            model = _mapper.Map<AdModel>(entity);
            return model;
        }
    }
}
