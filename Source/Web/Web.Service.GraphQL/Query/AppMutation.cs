using Core.Service.Interfaces;
using Core.Shared.DTO;
using GraphQL;
using GraphQL.Types;
using System;

namespace Web.Service.GraphQL
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(
            ISiteService siteService,
            IParticipationService participationService,
            IParticipantService participantService,
            IFailedTransactionService failedTransactionService)
        {
            Field<BooleanGraphType>(
                "createSite",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<SiteInputType>> { Name = "site" }),
                resolve: context =>
                {
                    var site = context.GetArgument<SiteDto>("site");
                    return siteService.CreateSite(site);
                }
            );

            Field<BooleanGraphType>(
                "updateSite",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SiteInputType>> { Name = "site" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "siteId" }),
                resolve: context =>
                {
                    var site = context.GetArgument<SiteDto>("site");
                    var siteId = context.GetArgument<Guid>("siteId");

                    var dbSite = siteService.GetSite(siteId);
                    if (dbSite == null)
                    {
                        context.Errors.Add(new ExecutionError("Couldn't find site in db."));
                        return null;
                    }

                    return siteService.UpdateSite(site);
                }
            );

            Field<BooleanGraphType>(
                "deleteSite",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "siteId" }),
                resolve: context =>
                {
                    var siteId = context.GetArgument<Guid>("siteId");
                    var site = siteService.GetSite(siteId);
                    if (site == null)
                    {
                        context.Errors.Add(new ExecutionError("Couldn't find site in db."));
                        return null;
                    }

                    siteService.DeleteSite(siteId);
                    return $"The site with the id: {siteId} has been successfully deleted from db.";
                }
            );
        }
    }
}
