using System;

namespace Admin.Dashboard.Razor.ViewModels
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
