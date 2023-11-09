using ItsLib.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsLib.DAL.Repositories
{
    public interface IUserDiscountCodeRepository : IRepository<UserDiscountCode>
    {

        public List<UserDiscountCode> GetAllInclude();
    }
}
