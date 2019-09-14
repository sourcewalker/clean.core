using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Interfaces.DAL
{
    public interface IFailedTransactionRepository
    {
        IEnumerable<FailedTransactionDto> GetAll();

        Task<IEnumerable<FailedTransactionDto>> GetAllAsync();

        IEnumerable<FailedTransactionDto> GetPaged(int pageNumber, int pageSize);

        Task<IEnumerable<FailedTransactionDto>> GetPagedAsync(int pageNumber, int pageSize);

        FailedTransactionDto GetById(Guid id);

        Task<FailedTransactionDto> GetByIdAsync(Guid id);

        bool Add(FailedTransactionDto failedTransaction);

        Task<bool> AddAsync(FailedTransactionDto failedTransaction);

        bool Update(FailedTransactionDto failedTransaction);

        Task<bool> UpdateAsync(FailedTransactionDto failedTransaction);

        bool Delete(Guid id);

        Task<bool> DeleteAsync(Guid id);
    }
}
