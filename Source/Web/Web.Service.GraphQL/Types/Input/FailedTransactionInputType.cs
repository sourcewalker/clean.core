using Core.Model;
using Core.Service.Interfaces;
using Core.Shared.DTO;
using GraphQL.Types;

namespace Web.Service.GraphQL
{
    public class FailedTransactionInputType : InputObjectGraphType<FailedTransactionDto>
    {
        public FailedTransactionInputType()
        {
            var typeName = nameof(FailedTransaction);
            Name = $"{typeName.ToLowerInvariant()}Input";
            Field<IdGraphType>("id");
            Field<BooleanGraphType>("termsConsent");
            Field<BooleanGraphType>("newsletterOptin");
            Field<IdGraphType>("participationId");
        }
    }
}
