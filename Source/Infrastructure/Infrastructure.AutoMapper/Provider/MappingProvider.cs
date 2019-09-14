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

        public BaseDto toDto(EntityBase<Guid> entity)
            => _mapper.Map<EntityBase<Guid>, BaseDto>(entity);

        public EntityBase<Guid> toEntity(BaseDto dto)
            => _mapper.Map<BaseDto, EntityBase<Guid>>(dto);

        public IEnumerable<BaseDto> toDtos(IEnumerable<EntityBase<Guid>> entities)
            => _mapper.Map<IEnumerable<EntityBase<Guid>>, IEnumerable<BaseDto>>(entities);

        public IEnumerable<EntityBase<Guid>> toEntities(IEnumerable<BaseDto> dtos)
            => _mapper.Map<IEnumerable<BaseDto>, IEnumerable<EntityBase<Guid>>>(dtos);
    }
}
