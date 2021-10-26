using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.County;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.County;
using Api.Domain.models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.services
{
    public class CountyService : ICountyService
    {
        private ICountyRepository _repository;

        private readonly IMapper _mapper;

        public CountyService(ICountyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<CountyDto> Get(Guid id)
        {
            var _entity = await _repository.SelectAsync(id);
            return _mapper.Map<CountyDto>(_entity);
        }

        public async Task<IEnumerable<CountyDtoComplete>> GetAll()
        {
            var _entities = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<CountyDtoComplete>>(_entities);
        }

        public async Task<CountyDtoComplete> GetCompleteByIBGE(int IBGECode)
        {
            var _entity = await _repository.GetCompleteByIBGE(IBGECode);
            return _mapper.Map<CountyDtoComplete>(_entity);
        }

        public async Task<CountyDtoComplete> GetCompleteById(Guid id)
        {
            var _entity = await _repository.GetCompleteById(id);
            return _mapper.Map<CountyDtoComplete>(_entity);
        }

        public async Task<CountyDtoCreateResult> Post(CountyDtoCreate county)
        {
            var _model = _mapper.Map<CountyModel>(county);
            var _entity = _mapper.Map<CountyEntity>(_model);
            var _insertResult = await _repository.InsertAsync(_entity);
            return _mapper.Map<CountyDtoCreateResult>(_insertResult);
        }

        public async Task<CountyDtoUpdateResult> Put(CountyDtoUpdate county)
        {
            var _model = _mapper.Map<CountyModel>(county);
            var _entity = _mapper.Map<CountyEntity>(_model);
            var _insertResult = await _repository.UpdateAsync(_entity);
            return _mapper.Map<CountyDtoUpdateResult>(_insertResult);
        }
    }
}
