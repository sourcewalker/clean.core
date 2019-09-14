using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Interfaces
{
    public interface ISurveyService
    {
        IEnumerable<ParticipationDto> ExtractParticipation(DateTimeOffset start, DateTimeOffset end);

        int GetParticipationNumberBySite(Guid siteId);
    }
}
