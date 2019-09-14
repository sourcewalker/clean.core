using Core.Model;
using Core.Shared.DTO;
using System;
using System.Collections.Generic;

namespace Core.Infrastructure.Interfaces.Mapping
{
    public interface IMappingProvider
    {
        BaseDto toDto(EntityBase<Guid> entity);

        EntityBase<Guid> toEntity(BaseDto dto);

        IEnumerable<BaseDto> toDtos(IEnumerable<EntityBase<Guid>> entities);

        IEnumerable<EntityBase<Guid>> toEntities(IEnumerable<BaseDto> dtos);
    }
}
