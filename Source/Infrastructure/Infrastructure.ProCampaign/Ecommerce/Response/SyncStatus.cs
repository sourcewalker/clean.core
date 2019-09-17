using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ProCampaign.Ecommerce.Response
{
    public class SyncStatus
    {
        public Guid Id { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }
    }
}
