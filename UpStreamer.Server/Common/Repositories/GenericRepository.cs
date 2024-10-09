using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using UpStreamer.Server.Database;

namespace UpStreamer.Server.Common.Repository
{

    public class GenericRepository<TEntity>(UpStreamerDbContext context) : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly UpStreamerDbContext _context = context;

        public void Create(TEntity model)
        {
            _context.Set<TEntity>().Add(model);
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
        {
            if(includes == null)
            {
                _context.Set<TEntity>().Where(predicate).AsNoTracking();
            }

            return includes!(_context.Set<TEntity>()).Where(predicate).AsNoTracking();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
