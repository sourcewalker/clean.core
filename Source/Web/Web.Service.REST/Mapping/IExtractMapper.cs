using Core.Shared.DTO;
using System.Collections.Generic;
using Web.Service.REST.Models;

namespace Web.Service.REST.Mapping
{
    public interface IExtractMapper
    {
        ExtractModel toExtractModel(ParticipationDto participation);

        IEnumerable<ExtractModel> toExtractModels(IEnumerable<ParticipationDto> participations);
    }
}
