using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Shared.DTO;
using Infrastructure.DAL.EF.Repository.Base;
using Core.Infrastructure.Interfaces.DAL;
using Core.Model;
using Core.Infrastructure.Interfaces.Mapping;

namespace Infrastructure.DAL.EF.Repository.Implementations
{
    public class FailedTransactionRepository : RepositoryBase<FailedTransaction>, IFailedTransactionRepository
    {
        private readonly IMappingProvider _mapper;

        public FailedTransactionRepository(IMappingProvider mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<FailedTransactionDto> GetAll()
            => _mapper.toDtos<FailedTransactionDto>(Table);

        public async Task<IEnumerable<FailedTransactionDto>> GetAllAsync()
            => await Task.Run(() => _mapper.toDtos<FailedTransactionDto>(Table));

        public FailedTransactionDto GetById(Guid id)
            => _mapper.toDto<FailedTransactionDto>(Find(id));

        public async Task<FailedTransactionDto> GetByIdAsync(Guid id)
            => await Task.Run(() => _mapper.toDto<FailedTransactionDto>(Find(id)));

        public IEnumerable<FailedTransactionDto> GetPaged(int pageNumber, int pageSize)
            => _mapper.toDtos<FailedTransactionDto>(GetRange(pageSize * (pageNumber - 1), pageSize));

        public async Task<IEnumerable<FailedTransactionDto>> GetPagedAsync(int pageNumber, int pageSize)
            => await Task.Run(() => _mapper.toDtos<FailedTransactionDto>(GetRange(pageSize * (pageNumber - 1), pageSize)));

        public bool Add(FailedTransactionDto failedTransaction)
        {
            failedTransaction.Id = failedTransaction.Id != default ? failedTransaction.Id : Guid.NewGuid();
            failedTransaction.CreatedDate = DateTimeOffset.UtcNow;
            return Add(_mapper.toEntity<FailedTransaction>(failedTransaction), true) > 0;
        }

        public async Task<bool> AddAsync(FailedTransactionDto failedTransaction)
        {
            failedTransaction.Id = failedTransaction.Id != default ? failedTransaction.Id : Guid.NewGuid();
            failedTransaction.CreatedDate = DateTimeOffset.UtcNow;
            return await Task.Run(() => Add(_mapper.toEntity<FailedTransaction>(failedTransaction), true) > 0);
        }

        public bool Update(FailedTransactionDto failedTransaction)
        {
            failedTransaction.ModifiedDate = DateTimeOffset.UtcNow;
            return Update(_mapper.toEntity<FailedTransaction>(failedTransaction), true) > 0;
        }

        public async Task<bool> UpdateAsync(FailedTransactionDto failedTransaction)
        {
            failedTransaction.ModifiedDate = DateTimeOffset.UtcNow;
            return await Task.Run(() => Update(_mapper.toEntity<FailedTransaction>(failedTransaction), true) > 0);
        }

        public bool Delete(FailedTransactionDto failedTransaction)
            => Delete(_mapper.toEntity<FailedTransaction>(failedTransaction), true) > 0;

        public async Task<bool> DeleteAsync(FailedTransactionDto failedTransaction)
            => await Task.Run(() => Delete(_mapper.toEntity<FailedTransaction>(failedTransaction), true) > 0);

        public bool Delete(Guid id)
        {
            var failedTransaction = Find(id);
            return Delete(failedTransaction, true) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
            => await Task.Run(() =>
            {
                var failedTransaction = Find(id);
                return Delete(failedTransaction, true) > 0;
            });
    }
}
