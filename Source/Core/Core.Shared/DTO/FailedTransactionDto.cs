using System;

namespace Core.Shared.DTO
{
    public class FailedTransactionDto
    {
        public Guid Id { get; set; }

        public Guid? ParticipationId { get; set; }

        public ParticipationDto Participation { get; set; }

        public string Retailer { get; set; }

        public string AlreadyTriedFlavours { get; set; }

        public bool TermsConsent { get; set; }

        public bool NewsletterOptin { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
