using Core.Model;
using Core.Shared.DTO;
using GraphQL.Types;

namespace Admin.Service.GraphQL
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
