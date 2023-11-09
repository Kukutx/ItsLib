using ItsLib.DAL.Data;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ItsLib.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LibDbContext _ctx;
        internal DbSet<T> _dbSet { get; set; }

        public GenericRepository(LibDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public IQueryable GetAll(ODataQueryOptions<T> options)
        {
            return options.ApplyTo(_dbSet.AsQueryable<T>());
        }

        public T Create(T entity)
        {
            _ctx.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public bool Delete(Guid id)
        {
            _ctx.Remove(Get(id));
            return _ctx.SaveChanges() > 0;
        }

        public T? Get(Guid id)
        {
            return _ctx.Find<T>(id);
        }

        public T? GetUserProduct(string userId, Guid id)
        {
            return _ctx.Find<T>(userId, id);
        }


        public T? Get(string id)
        {
            return _ctx.Find<T>(id);
        }

        public T? Get(Func<T, bool> query, string include, string b, string c)
        {
            return _dbSet.Include(include).Include(b).Include(c).SingleOrDefault(query);
        }

        public T? Get(Func<T, bool> query, string include)
        {
            return _dbSet.Include(include).FirstOrDefault(query);
        }


        public List<T> GetByFilter(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void DeleteAll()
        {
            _dbSet.RemoveRange(_ctx.Set<T>().ToList());
        }

        public virtual bool Update(T entity)
        {
            _dbSet.Update(entity);
            return _ctx.SaveChanges() > 0;
        }


    }
}
