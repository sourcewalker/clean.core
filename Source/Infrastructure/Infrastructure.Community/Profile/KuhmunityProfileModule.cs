using Infrastructure.Community.Helper;
using Infrastructure.Community.Interfaces;
using Infrastructure.Community.Models;
using Microsoft.Extensions.Configuration;
using SWEET.WebProjects.Brands.Milka.Kuhmunity.DTO;

namespace Infrastructure.Community.Profile
{
    public class KuhmunityProfileModule : KuhmunityBase, IProfileProvider
    {
        private UserOperationInput _userOperationData;
        private readonly IConfiguration _configuration;

        public KuhmunityProfileModule(IConfiguration configuration, UserOperationInput operationData)
        {
            _configuration = configuration;
            _userOperationData = operationData;
            operationData.UserId = GetUserIdFromSessionTicket(operationData.SessionTicket);
        }

        public KuhmunityResponse GetProfile()
        {
            return KuhmunityProfileHelper.GetProfile(GetKuhmunityApiEndpoint(_configuration), _userOperationData);
        }
    }
}
