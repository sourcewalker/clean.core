using System;

namespace Core.Shared.DTO
{
    public class FailedTransactionDto : BaseDto
    {

        public Guid? ParticipationId { get; set; }

        public ParticipationDto Participation { get; set; }

        public bool TermsConsent { get; set; }

        public bool NewsletterOptin { get; set; }
    }
}
