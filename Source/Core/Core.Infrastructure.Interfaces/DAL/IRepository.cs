using Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Interfaces.DAL
{
    public interface IRepository<TEntity, TId>
        where TEntity : EntityBase<TId>
        where TId : struct
    {
        int Count { get; }
        bool HasChanges { get; }

        #region Query

        TEntity Find(TId? id);
        TEntity GetFirst();
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetRange(int skip, int take);

        #endregion

        #region Command

        int Add(TEntity entity, bool persist = true);
        int AddRange(IEnumerable<TEntity> entities, bool persist = true);

        int Update(TEntity entity, bool persist = true);
        int UpdateRange(IEnumerable<TEntity> entities, bool persist = true);

        int Delete(TEntity entity, bool persist = true);
        int DeleteRange(IEnumerable<TEntity> entities, bool persist = true);
        int Delete(TId id, byte[] timeStamp, bool persist = true);

        int SaveChanges();

        #endregion
    }
}
