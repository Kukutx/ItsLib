using ItsLib.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItsLib.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(LibDbContext ctx) : base(ctx)
        {
        }


        public override bool Update(Category entity)
        {
            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.Update(entity);
            return _ctx.SaveChanges() > 0;
        }

        public bool Patch([FromBody] Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Category> patch, Guid id)
        {

            var currentCategory = this.Get(id);

            patch.ApplyTo(currentCategory);

            return this._ctx.SaveChanges() > 0;

        }
    }
}
