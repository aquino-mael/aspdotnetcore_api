using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.Uf;
using AutoMapper;

namespace Api.Service.services
{
    public class UfService : IUfService
    {
        private IRepository<UfEntity> _repository;

        private readonly IMapper _mapper;

        public UfService(IRepository<UfEntity> repository)
        {
            _repository = repository;
        }

        public async Task<UfDto> Get(Guid id)
        {
            var _entity = await _repository.SelectAsync(id);
            return _mapper.Map<UfDto>(_entity);
        }

        public async Task<IEnumerable<UfDto>> GetAll()
        {
            var _entities = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<UfDto>>(_entities);
        }
    }
}
