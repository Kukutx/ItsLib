using ItsLib.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItsLib.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(LibDbContext ctx) : base(ctx)
        {
        }

        public List<Product> GetAllInclude()
        {
            return _dbSet.Include("Category").ToList();
        }

        public override bool Update(Product entity)
        {
            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.Update(entity);
            return _ctx.SaveChanges() > 0;
        }

        public bool Patch([FromBody] Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Product> patch, Guid id)
        {

            var currentProduct = this.Get(product => product.ProductId == id, "ProductUsers");

            patch.ApplyTo(currentProduct);

            return this._ctx.SaveChanges() > 0;

        }
    }
}
