using Core.Service.Interfaces;
using GraphQL;
using GraphQL.Types;
using System;

namespace Web.Service.GraphQL
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(
            ISiteService siteService,
            IParticipationService participationService,
            IParticipantService participantService,
            IFailedTransactionService failedTransactionService)
        {
            Field<ListGraphType<SiteType>>(
               "sitesPaged",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "pageNumber" },
                   new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "pageSize" }),
               resolve: context => 
               {
                   var pageNumber = context.GetArgument<int>("pageNumber");
                   var pageSize = context.GetArgument<int>("pageSize");
                   return siteService.GetSitesPaged(pageNumber, pageSize);
               }
            );

            Field<SiteType>(
                "site",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "siteId" },
                    new QueryArgument<StringGraphType> { Name = "siteCulture" },
                    new QueryArgument<StringGraphType> { Name = "siteDomain" }
                ),
                resolve: context =>
                {
                    Guid id = Guid.Empty;
                    if (context.GetArgument<string>("siteId") != null && 
                        !Guid.TryParse(context.GetArgument<string>("siteId"), out id))
                    {
                        context.Errors.Add(new ExecutionError("Wrong value for guid id"));
                        return null;
                    }
                    if (context.GetArgument<string>("siteId") != null)
                    {
                        return siteService.GetSite(id);
                    }

                    var culture = context.GetArgument<string>("siteCulture");
                    if (!string.IsNullOrEmpty(culture))
                    {
                        return siteService.GetSiteByCulture(culture);
                    }

                    var domain = context.GetArgument<string>("siteDomain");
                    if (!string.IsNullOrEmpty(domain))
                    {
                        return siteService.GetSiteByDomain(domain);
                    }

                    context.Errors.Add(new ExecutionError("Please provide at least one parameter."));
                    return null;
                }
            );
        }
    }
}
