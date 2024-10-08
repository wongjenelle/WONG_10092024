using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UpStreamer.Server.Database
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

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, int skip, int offset)
        {
            return _context.Set<TEntity>().Where(predicate).AsNoTracking();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }   

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
