using ItsLib.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsLib.DAL.Repositories
{
    public class DiscountCodeRepository : GenericRepository<DiscountCode>, IDiscountCodeRepository
    {

        public DiscountCodeRepository(LibDbContext ctx) : base(ctx)
        {
        }

        public int NewDiscount()
        {
            Random random = new Random();
            int ris = random.Next(5, 31);
            return ris;
        }

        public string NewDiscountCode()
        {
            Random random = new Random();
            int lunghezza = random.Next(6, 9);
            const string caratteri = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            char[] codiceArray = new char[lunghezza];
            for (int i = 0; i < lunghezza; i++)
            {
                codiceArray[i] = caratteri[random.Next(caratteri.Length)];
            }
            return new string(codiceArray);
        }

        public override bool Update(DiscountCode entity)
        {
            _ctx.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.Update(entity);
            return _ctx.SaveChanges() > 0;
        }
    }
}
