using AutoMapper;
using Core.Shared.DTO;
using System.Collections.Generic;
using Web.Service.REST.Models;

namespace Web.Service.REST.Mapping.Helper
{
    public class ExtractMapper
    {
        private readonly IMapper _mapper;

        public ExtractMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ExtractModel toExtractModel(ParticipationDto participation)
            => _mapper.Map<ParticipationDto, ExtractModel>(participation);

        public IEnumerable<ExtractModel> toExtractModels(IEnumerable<ParticipationDto> participations)
            => _mapper.Map<IEnumerable<ParticipationDto>, IEnumerable<ExtractModel>>(participations);
    }
}
