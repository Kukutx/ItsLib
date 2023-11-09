using ItsLib.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ItsLib.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LibDbContext ctx) : base(ctx) { }

        public string NewLoyaltyCardCode()
        {
            Random rnd = new Random();
            long month = rnd.NextInt64(10000000000000, 99999999999999);
            string ris = Convert.ToString(month);
            return ris;
        }

        public override bool Update(User entity)
        {
            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.Update(entity);
            return _ctx.SaveChanges() > 0;
        }
    }
}
