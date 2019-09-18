using Core.Model;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using GraphQL.Types;

namespace Web.Service.GraphQL
{
    public class ParticipantInputType : InputObjectGraphType<ParticipantDto>
    {
        public ParticipantInputType()
        {
            var typeName = nameof(Participant);
            Name = $"{typeName.ToLowerInvariant()}Input";
            Field<IdGraphType>("id");
            Field<StringGraphType>("consumerId");
            Field<StringGraphType>("emailHash");
            Field<IdGraphType>("participationId");
        }
    }
}
