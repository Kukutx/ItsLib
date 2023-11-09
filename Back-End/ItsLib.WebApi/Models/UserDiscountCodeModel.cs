using ItsLib.DAL.Data;

namespace ItsLib.WebApi.Models
{
    public class UserDiscountCodeModel
    {
        public Guid DiscountCodeId { get; set; }
        public string UserId { get; set; }
        public bool IsDisabled { get; set; }
        public UserModel User { get; set; }
        public DiscountCode DiscountCode { get; set; }
    }

    public class PostUserDiscountCodeModel
    {
        public Guid DiscountCodeId { get; set; }
        public string UserId { get; set; }
    }
}
