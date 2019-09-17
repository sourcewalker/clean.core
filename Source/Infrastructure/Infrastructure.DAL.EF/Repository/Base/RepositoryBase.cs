using Core.Infrastructure.Interfaces.DAL;
using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.DAL.EF.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepository<TEntity, Guid>
        where TEntity : EntityBase<Guid>, new()
    {
        bool _disposed = false;

        protected readonly DatabaseContext context;

        protected DbSet<TEntity> entities;

        protected RepositoryBase(DatabaseContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }

        public DatabaseContext Context 
            => context;

        public bool HasChanges 
            => context.ChangeTracker.HasChanges();

        public int Count 
            => entities.Count();

        public TEntity GetFirst() 
            => entities.FirstOrDefault();

        public TEntity Find(Guid? id) 
            => entities.Find(id);

        public virtual IEnumerable<TEntity> GetAll() 
            => entities.AsEnumerable();

        internal IEnumerable<TEntity> GetRange(IQueryable<TEntity> query, int skip, int take)
            => query.Skip(skip).Take(take);

        public virtual IEnumerable<TEntity> GetRange(int skip, int take)
            => GetRange(entities, skip, take);

        public virtual int Add(TEntity entity, bool persist = true)
        {
            entities.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int AddRange(IEnumerable<TEntity> entitie_s, bool persist = true)
        {
            foreach (var entity in entitie_s)
            {
                entities.Add(entity);
            }
            return persist ? SaveChanges() : 0;
        }
        public virtual int Update(TEntity entity, bool persist = true)
        {
            context.Entry(entity).State = EntityState.Modified;
            return persist ? SaveChanges() : 0;
        }
        public virtual int UpdateRange(IEnumerable<TEntity> entitie_s, bool persist = true)
        {
            foreach (var entity in entitie_s)
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            return persist ? SaveChanges() : 0;
        }
        public virtual int Delete(TEntity entity, bool persist = true)
        {
            entities.Remove(entity);
            return persist ? SaveChanges() : 0;
        }
        public virtual int DeleteRange(IEnumerable<TEntity> entitie_s, bool persist = true)
        {
            foreach (var entity in entitie_s)
            {
                entities.Remove(entity);
            }
            return persist ? SaveChanges() : 0;
        }

        public int Delete(Guid id, byte[] timeStamp, bool persist = true)
        {
            var entity = Find(id);
            context.Entry(entity).State = EntityState.Deleted;
            return persist ? SaveChanges() : 0;
        }

        public int SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception($"{ex.HResult}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            context.Dispose();
            _disposed = true;
        }
    }
}
