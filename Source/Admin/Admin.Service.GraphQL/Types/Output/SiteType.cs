using Core.Model;
using Core.Shared.DTO;
using GraphQL.Types;

namespace Admin.Service.GraphQL
{
    public class SiteType : ObjectGraphType<SiteDto>
    {
        public SiteType()
        {
            var typeName = nameof(Site);

            Field(x => x.Id, type: typeof(IdGraphType))
                .Description($"Id property from the {typeName} object.");
            Field(x => x.Name, type: typeof(StringGraphType))
                .Description($"Name property from the {typeName} object.");
            Field(x => x.Culture, type: typeof(StringGraphType))
                .Description($"Culture property from the {typeName} object.");
            Field(x => x.Domain, type: typeof(StringGraphType))
                .Description($"Domain property from the {typeName} object.");
            Field(x => x.CreatedDate, type: typeof(DateTimeOffsetGraphType))
                .Description($"Created date property from the {typeName} object.");
            Field(x => x.ModifiedDate, type: typeof(DateTimeOffsetGraphType))
                .Description($"Modified date property from the {typeName} object.");
        }
    }
}
