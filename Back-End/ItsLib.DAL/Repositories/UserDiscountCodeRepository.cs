using ItsLib.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsLib.DAL.Repositories
{
    public class UserDiscountCodeRepository : GenericRepository<UserDiscountCode>, IUserDiscountCodeRepository
    {

        public UserDiscountCodeRepository(LibDbContext ctx) : base(ctx)
        {
        }

        public List<UserDiscountCode> GetAllInclude()
        {
            return _dbSet.Include("User").Include("DiscountCode").ToList();
        }

        public override bool Update(UserDiscountCode entity)
        {
            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.Update(entity);
            return _ctx.SaveChanges() > 0;
        }
    }
}
