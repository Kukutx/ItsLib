using ItsLib.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsLib.DAL.Repositories
{
    public interface ILibOfWork
    {
        ICategoryRepository CategoryRepo { get; }
        IDiscountCodeRepository DiscountCodeRepo { get; }
        IProductRepository ProductRepo { get; }
        IProductUserRepository ProductUserRepo { get; }
        IUserRepository UserRepo { get; }
        IUserDiscountCodeRepository UserDiscountCodeRepo { get; }
        bool Commit();
    }
}
