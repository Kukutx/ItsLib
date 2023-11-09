using ItsLib.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsLib.DAL.Repositories
{
    public interface IDiscountCodeRepository : IRepository<DiscountCode>
    {
        public int NewDiscount();

        public string NewDiscountCode();
    }
}
