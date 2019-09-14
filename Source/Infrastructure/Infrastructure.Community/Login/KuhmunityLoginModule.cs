using Infrastructure.Community.Helper;
using Infrastructure.Community.Interfaces;
using Infrastructure.Community.Models;
using Microsoft.Extensions.Configuration;
using SWEET.WebProjects.Brands.Milka.Kuhmunity.DTO;

namespace Infrastructure.Community.Login
{
    public partial class KuhmunityLoginModule : KuhmunityBase, ILoginProvider
    {
        private readonly IConfiguration _configuration;

        public KuhmunityLoginModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CampaignID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IsPersistentCookie { get; set; }
        public string Culture { get; set; }
        public string SessionTicket { get; set; }

        private UserDetailDTO UserDetail;


        public KuhmunityResponse Login()
        {
            var status = KuhmunityLoginHelper.Login(
                GetKuhmunityApiEndpoint(_configuration),
                CampaignID,
                Email,
                Password,
                IsPersistentCookie,
                SessionTicket,
                UserDetail);

            SessionTicket = status.sessionTicket;
            UserDetail = status.userDetail;
            return status.response;
        }

        public string GetLoginCookie()
        {
            return new KuhmunityCookie
            {
                SessionTicket = this.SessionTicket,
                SessionUserName = UserDetail.Nickname,
                SessionProfilePicture = UserDetail.ProfilePictureUrl,
                SessionBackgroundUrl = UserDetail.BackGroundUrl
            }.ToString();
        }
    }
}
