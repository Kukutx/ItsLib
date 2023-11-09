using ItsLib.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ItsLib.DAL.Repositories
{
    public class ProductUserRepository : GenericRepository<ProductUser>, IProductUserRepository
    {

        public ProductUserRepository(LibDbContext ctx) : base(ctx)
        {
        }


        public int ReviewNumber(string userId)
        {
            int n = this.GetAll().FindAll(x => x.UserId == userId && x.Review != null && x.ReviewTitle != null).Count();
            return n + 1;

        }

        public override bool Update(ProductUser entity)
        {
            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.Update(entity);
            return _ctx.SaveChanges() > 0;
        }
    }
}
