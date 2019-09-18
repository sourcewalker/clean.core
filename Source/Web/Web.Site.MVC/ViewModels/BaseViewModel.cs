using System;

namespace Web.Site.MVC.ViewModels
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
