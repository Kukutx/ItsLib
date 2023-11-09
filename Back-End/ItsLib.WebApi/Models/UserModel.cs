using ItsLib.DAL.Data;

namespace ItsLib.WebApi.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LoyaltyCardCode { get; set; }
        public bool IsDisabled { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public List<UserDiscountCode> UserDiscountCodes { get; set; }
    }
}
