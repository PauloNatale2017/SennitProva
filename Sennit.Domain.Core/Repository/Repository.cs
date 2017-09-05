using Sennit.Domain.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sennit.Domain.Core.Repository
{
    public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {

        private readonly Sennit.DataAccessLayer.dbContext.dbContext _ctx;
        internal DbSet<TEntity> _dbSet;

        public Repository(Sennit.DataAccessLayer.dbContext.dbContext ctx)
        {
            this._ctx = ctx;
            this._dbSet = _ctx.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _ctx.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public TEntity Find(params object[] key)
        {
            return _ctx.Set<TEntity>().Find(key);
        }

        public void Update(TEntity obj)
        {
            _ctx.Set<TEntity>().Attach(obj);
            _ctx.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void SaveAll()
        {
            _ctx.SaveChanges();
        }

        public void Add(TEntity obj)
        {
            _ctx.Set<TEntity>().Add(obj);
            _ctx.SaveChanges();

        }

        public void Exclude(Func<TEntity, bool> predicate)
        {
            _ctx.Set<TEntity>()
                .Where(predicate).ToList()
                .ForEach(del => _ctx.Set<TEntity>().Remove(del));
        }


        public void AddAllEntity(List<TEntity> entity)
        {
            foreach (var item in entity) { _dbSet.Add(item); }
            _ctx.SaveChanges();
        }

        public void DeleteAllEntity(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            List<TEntity> listDelete = query.Where(filter).ToList();
            foreach (var item in listDelete) { _dbSet.Remove(item); }
            _ctx.SaveChanges();
        }

        public TEntity Authenticate(params object[] key)
        {
            return _ctx.Set<TEntity>().Find(key);
        }

        public void Dispose()
        {
            _dbSet = null;
            _ctx.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
