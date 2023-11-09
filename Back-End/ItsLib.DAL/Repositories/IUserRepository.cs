using ItsLib.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsLib.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public string NewLoyaltyCardCode();
    }
}
