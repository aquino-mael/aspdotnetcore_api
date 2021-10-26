using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.services
{
    public class UfService : IUfService
    {
        private IUfRepository _repository;

        private readonly IMapper _mapper;

        public UfService(IUfRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
