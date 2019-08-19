using System;

namespace Core.Shared.DTO
{
    public class SiteDto
    {
        public Guid Id { get; set; }

        public string Culture { get; set; }

        public string Name { get; set; }

        public string Domain { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
