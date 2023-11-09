using Microsoft.AspNetCore.OData.Query;

namespace ItsLib.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T? Get(Guid id);
        T? Get(string id);
        T? Get(Func<T, bool> query, string include);
        T? Get(Func<T, bool> query, string include, string b, string c);
        List<T> GetAll();
        T Create(T entity);
        bool Delete(Guid id);
        List<T> GetByFilter(Func<T, bool> predicate);
        void DeleteAll();
        IQueryable GetAll(ODataQueryOptions<T> options);

        public T? GetUserProduct(string userId, Guid id);

    }
}
