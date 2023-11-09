using Microsoft.EntityFrameworkCore;

namespace ItsLib.DAL.Data
{
    [PrimaryKey(nameof(DiscountCodeId))]
    public class DiscountCode
    {
        public Guid DiscountCodeId { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public bool IsDisabled { get; set; }
        public List<UserDiscountCode> UserDiscountCodes { get; set; }
    }
}
