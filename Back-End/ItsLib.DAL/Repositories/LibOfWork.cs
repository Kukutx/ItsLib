using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItsLib.DAL.Data;

namespace ItsLib.DAL.Repositories
{
    public class LibOfWork : ILibOfWork
    {
        private readonly LibDbContext _ctx;
        public ICategoryRepository CategoryRepo { get; private set; }
        public IDiscountCodeRepository DiscountCodeRepo { get; private set; }
        public IProductRepository ProductRepo { get; private set; }
        public IProductUserRepository ProductUserRepo { get; private set; }
        public IUserRepository UserRepo { get; private set; }
        public IUserDiscountCodeRepository UserDiscountCodeRepo { get; private set; }


        public LibOfWork(LibDbContext ctx)
        {
            _ctx = ctx;
            CategoryRepo = new CategoryRepository(ctx);
            DiscountCodeRepo = new DiscountCodeRepository(ctx);
            ProductRepo = new ProductRepository(ctx);
            ProductUserRepo = new ProductUserRepository(ctx);
            UserRepo = new UserRepository(ctx);
            UserDiscountCodeRepo = new UserDiscountCodeRepository(ctx);
        }

        public bool Commit()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
