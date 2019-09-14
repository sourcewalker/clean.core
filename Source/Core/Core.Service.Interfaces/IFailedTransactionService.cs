using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Service.Interfaces
{
    public interface IFailedTransactionService
    {
        bool Create(FailedTransactionDto transaction);

        bool Delete(Guid id);
    }
}
