using ItsLib.DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace ItsLib.DAL.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetAllInclude();

        public bool Patch([FromBody] Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Product> patch, Guid id);
    }
}
