using GraphQL.Client;
using GraphQL.Common.Request;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Dashboard.Razor.Client;
using Admin.Dashboard.Razor.ViewModels;

namespace Admin.Dashboard.Razor.GraphQL
{
    public class GraphQlClient : IServiceClient
    {
        private readonly GraphQLClient _client;

        public GraphQlClient(GraphQLClient client)
        {
            _client = client;
        }

        public async Task<List<SiteViewModel>> GetAllSitesAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                query sitesQuery{
                  sites {
                    id
                    name
                    culture
                    domain
                  }
                }"
            };

            var result = await _client.PostAsync(query);
            return result.GetDataFieldAs<List<SiteViewModel>>("sites");
        }

        public async Task<SiteViewModel> GetSiteByCultureAsync(string culture)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                query siteQuery($siteCulture: ID!) {
                  site(siteCulture: $siteCulture) {
                    id
                    name
                    culture
                    domain
                  }
                }",
                Variables = new { siteDomain = culture }
            };

            var result = await _client.PostAsync(query);
            return result.GetDataFieldAs<SiteViewModel>("site");
        }
    }
}
