using System;

namespace Core.Model
{
    public class Site : EntityBase<Guid>
    {
        public string Culture { get; set; }

        public string Name { get; set; }

        public string Domain { get; set; }
    }
}
