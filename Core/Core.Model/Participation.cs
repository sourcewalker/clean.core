using System;

namespace Core.Model
{
    public class Participation : EntityBase<Guid>
    {
        public Guid? SiteId { get; set; }

        public Site Site { get; set; }

        public string Status { get; set; }

        public string ApiStatus { get; set; }

        public string ApiMessage { get; set; }
    }
}
