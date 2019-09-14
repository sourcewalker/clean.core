namespace Infrastructure.Community.Models
{
    public class KuhmunityCookie
    {
        public string SessionTicket { get; set; }
        public string SessionUserName { get; set; }
        public string SessionProfilePicture { get; set; }
        public string SessionBackgroundUrl { get; set; }

        public static KuhmunityCookie Parse(string value)
        {
            var components = value.Split('&');
            var newCookie = new KuhmunityCookie();

            foreach (string item in components)
            {
                var itemParts = item.Split('=');
                typeof(KuhmunityCookie).GetProperty(itemParts[0]).SetValue(newCookie, itemParts[1]);

            }

            return newCookie;
        }

        public override string ToString()
        {
            string[] cookieValue =
            {
                $"SessionTicket={SessionTicket}",
                $"SessionUserName={SessionUserName}",
                $"SessionProfilePicture={SessionProfilePicture}",
                $"SessionBackgroundUrl={SessionBackgroundUrl}"
            };

            return string.Join("&", cookieValue);
        }
    }
}
