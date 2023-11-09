using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsLib.DAL.Repositories
{
    public interface IRepository<T> : IGenericRepository<T> where T : class
    {
        bool Update(T entity);   
    }
}
