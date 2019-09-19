using GraphQL.Client;
using GraphQL.Common.Request;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Dashboard.Razor.Client;
using Admin.Dashboard.Razor.ViewModels;
using System;

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

        public async Task<SiteViewModel> CreateSiteAsync(SiteViewModel siteToCreate)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                mutation($site: siteInput!){
                  createSite(site: $site){
                    id
                    name
                    culture
                    domain
                  }
                }",
                Variables = new { site = siteToCreate }
            };

            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<SiteViewModel>("createSite");
        }

        public async Task<SiteViewModel> UpdateSiteAsync(Guid id, SiteViewModel siteToUpdate)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                mutation($site: siteInput!, $siteId: ID!){
                  updateSite(site: $site, siteId: $siteId){
                    id
                    name
                    culture
                    domain
                  }
               }",
                Variables = new { site = siteToUpdate, siteId = id }
            };

            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<SiteViewModel>("updateSite");
        }

        public async Task<string> DeleteSiteAsync(Guid id)
        {
            var query = new GraphQLRequest
            {
                Query = @"
               mutation($siteId: ID!){
                  deleteSite(siteId: $siteId)
                }",
                Variables = new { siteId = id }
            };

            var response = await _client.PostAsync(query);
            return response.Data.deleteSite;
        }
    }
}
