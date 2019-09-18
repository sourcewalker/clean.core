using Core.Model;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using GraphQL.Types;

namespace Web.Service.GraphQL
{
    public class ParticipantType : ObjectGraphType<ParticipantDto>
    {
        public ParticipantType(IParticipationService participationService)
        {
            var typeName = nameof(Participant);

            Field(x => x.Id, type: typeof(IdGraphType))
                .Description($"Id property from the {typeName} object.");
            Field(x => x.ParticipationId, type: typeof(IdGraphType))
                .Description($"Participation Id property from the {typeName} object.");
            Field(x => x.ConsumerId, type: typeof(StringGraphType))
                .Description($"Consumer Id property from the {typeName} object.");
            Field(x => x.EmailHash, type: typeof(StringGraphType))
                .Description($"Email Hash property from the {typeName} object.");
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
