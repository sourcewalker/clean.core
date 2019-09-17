using Core.Model;
using Core.Shared.DTO;
using System;
using System.Collections.Generic;

namespace Core.Infrastructure.Interfaces.Mapping
{
    public interface IMappingProvider
    {
        TDto toDto<TDto>(EntityBase<Guid> entity) where TDto : BaseDto;

        TEntity toEntity<TEntity>(BaseDto dto) where TEntity : EntityBase<Guid>;

        IEnumerable<TDto> toDtos<TDto>(IEnumerable<EntityBase<Guid>> entities) where TDto : BaseDto;

        IEnumerable<TEntity> toEntities<TEntity>(IEnumerable<BaseDto> dtos) where TEntity : EntityBase<Guid>;
    }
}
