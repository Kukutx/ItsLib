using ItsLib.DAL.Data;

namespace ItsLib.WebApi.Models
{
    public class DiscountCodeModel
    {
        public Guid DiscountCodeId { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public bool IsDisabled { get; set; }
        public List<UserDiscountCode> UserDiscountCodes { get; set; }
    }

}
