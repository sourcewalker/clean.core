using Core.Model;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using GraphQL.Types;

namespace Web.Service.GraphQL
{
    public class FailedTransactionType : ObjectGraphType<FailedTransactionDto>
    {
        public FailedTransactionType(IParticipationService participationService)
        {
            var typeName = nameof(FailedTransaction);

            Field(x => x.Id, type: typeof(IdGraphType))
                .Description($"Id property from the {typeName} object.");
            Field(x => x.ParticipationId, type: typeof(IdGraphType))
                .Description($"Participation Id property from the {typeName} object.");
            Field(x => x.TermsConsent, type: typeof(BooleanGraphType))
                .Description($"Terms Content status property from the {typeName} object.");
            Field(x => x.NewsletterOptin, type: typeof(BooleanGraphType))
                .Description($"Newsletter optin status property from the {typeName} object.");
            Field(x => x.CreatedDate, type: typeof(DateTimeOffsetGraphType))
                .Description($"Created date property from the {typeName} object.");
            Field(x => x.ModifiedDate, type: typeof(DateTimeOffsetGraphType))
                .Description($"Modified date property from the {typeName} object.");
            Field<ParticipationType>(
                "participation",
                resolve: context => participationService.GetParticipation(context.Source.ParticipationId.Value)
            );
        }
    }
}
