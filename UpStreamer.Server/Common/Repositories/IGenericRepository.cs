using System.Linq.Expressions;

namespace UpStreamer.Server.Common.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        void Create(T entity);
        T? Get(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetList(Expression<Func<T, bool>> predicate, int skip, int offset);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
