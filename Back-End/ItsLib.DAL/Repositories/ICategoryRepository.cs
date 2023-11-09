using ItsLib.DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace ItsLib.DAL.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {

        public bool Patch([FromBody] Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Category> patch, Guid id);
    }
}
