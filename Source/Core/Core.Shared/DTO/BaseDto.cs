using System;

namespace Core.Shared.DTO
{
    public class BaseDto
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
