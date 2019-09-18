using Core.Model;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using GraphQL.Types;

namespace Admin.Service.GraphQL
{
    public class ParticipationInputType : InputObjectGraphType<ParticipationDto>
    {
        public ParticipationInputType()
        {
            var typeName = nameof(Participation);
            Name = $"{typeName.ToLowerInvariant()}Input";
            Field<IdGraphType>("id");
            Field<StringGraphType>("status");
            Field<StringGraphType>("apiStatus");
            Field<StringGraphType>("apiMessage");
            Field<IdGraphType>("siteId");
        }
    }
}
