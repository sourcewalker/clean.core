using System;

namespace Core.Shared.DTO
{
    public class ParticipantDto : BaseDto
    {
        public string EmailHash { get; set; }

        public Guid? ParticipationId { get; set; }

        public ParticipationDto Participation { get; set; }

        public string ConsumerId { get; set; }

        public string Status { get; set; }

        public string ApiStatus { get; set; }

        public string ApiMessage { get; set; }
    }
}
