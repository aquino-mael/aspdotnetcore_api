using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.ZipCode;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.ZipCode;
using Api.Domain.models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.services
{
    public class ZipCodeService : IZipCodeService
    {
        private IZipCodeRepository _repository;
        private readonly IMapper _mapper;
        public ZipCodeService(IZipCodeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ZipCodeDto> Get(Guid id)
        {
            var _entity = await _repository.SelectAsync(id);
            return _mapper.Map<ZipCodeDto>(_entity);
        }

        public async Task<ZipCodeDto> Get(string cep)
        {
            var _entity = await _repository.SelectAsync(cep);
            return _mapper.Map<ZipCodeDto>(_entity);
        }

        public async Task<ZipCodeDtoCreateResult> Post(ZipCodeDtoCreate cep)
        {
            var _model = _mapper.Map<ZipCodeModel>(cep);
            var _entity = _mapper.Map<ZipCodeEntity>(_model);
            var _insertResult = await _repository.InsertAsync(_entity);
            return _mapper.Map<ZipCodeDtoCreateResult>(_insertResult);
        }

        public async Task<ZipCodeDtoUpdateResult> Put(ZipCodeDtoUpdate cep)
        {
            var _model = _mapper.Map<ZipCodeModel>(cep);
            var _entity = _mapper.Map<ZipCodeEntity>(_model);
            var _insertResult = await _repository.UpdateAsync(_entity);
            return _mapper.Map<ZipCodeDtoUpdateResult>(_insertResult);
        }
    }
}
