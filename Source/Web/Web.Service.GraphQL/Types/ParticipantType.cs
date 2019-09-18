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
            var typeName = typeof(ParticipantDto).GetType().Name;

            Field(x => x.Id, type: typeof(IdGraphType))
                .Description($"Id property from the {typeName} object.");
            Field(x => x.ParticipationId, type: typeof(IdGraphType))
                .Description($"Name property from the {typeName} object.");
            Field(x => x.ConsumerId, type: typeof(StringGraphType))
                .Description($"Culture property from the {typeName} object.");
            Field(x => x.EmailHash, type: typeof(StringGraphType))
                .Description($"Culture property from the {typeName} object.");
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
