using Core.Model;
using Core.Shared.DTO;
using GraphQL.Types;

namespace Admin.Service.GraphQL
{
    public class SiteInputType : InputObjectGraphType<SiteDto>
    {
        public SiteInputType()
        {
            var typeName = nameof(Site);
            Name = $"{typeName.ToLowerInvariant()}Input";
            Field<IdGraphType>("id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("culture");
            Field<StringGraphType>("domain");
        }
    }
}
