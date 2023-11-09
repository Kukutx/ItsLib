using ItsLib.DAL.Data;

namespace ItsLib.DAL.Repositories
{
    public interface IProductUserRepository : IRepository<ProductUser>
    {
        public int ReviewNumber(string userId);
    }
}
