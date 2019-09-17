using AutoMapper;
using Core.Infrastructure.Interfaces.Mapping;
using Core.Model;
using Core.Shared.DTO;
using System;
using System.Collections.Generic;

namespace Infrastructure.AutoMapper.Provider
{
    public class MappingProvider : IMappingProvider
    {
        private readonly IMapper _mapper;

        public MappingProvider(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDto toDto<TDto>(EntityBase<Guid> entity) where TDto : BaseDto
            => _mapper.Map<EntityBase<Guid>, TDto>(entity);

        public TEntity toEntity<TEntity>(BaseDto dto) where TEntity : EntityBase<Guid>
            => _mapper.Map<BaseDto, TEntity>(dto);

        public IEnumerable<TDto> toDtos<TDto>(IEnumerable<EntityBase<Guid>> entities) where TDto : BaseDto
            => _mapper.Map<IEnumerable<EntityBase<Guid>>, IEnumerable<TDto>>(entities);

        public IEnumerable<TEntity> toEntities<TEntity>(IEnumerable<BaseDto> dtos) where TEntity : EntityBase<Guid>
            => _mapper.Map<IEnumerable<BaseDto>, IEnumerable<TEntity>>(dtos);
    }
}
