using Core.Service.Interfaces;
using Core.Shared.DTO;
using System;
using System.Collections.Generic;

namespace Core.Service.Flow
{
    public class SurveyService : ISurveyService
    {
        private readonly IParticipationService _participationService;

        public SurveyService(IParticipationService participationService)
        {
            _participationService = participationService;
        }

        public IEnumerable<ParticipationDto> ExtractParticipation(DateTimeOffset start, DateTimeOffset end)
        {
            return _participationService.GetBetween(start, end);
        }

        public int GetParticipationNumberBySite(Guid siteId)
        {
            return _participationService.GetTotalParticipationNumberBySite(siteId);
        }
    }
}
