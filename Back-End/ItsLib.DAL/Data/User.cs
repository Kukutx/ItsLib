using Microsoft.AspNetCore.Identity;

namespace ItsLib.DAL.Data
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FiscalCode { get; set; }
        public string LoyaltyCardCode { get; set; }
        public bool IsDisabled { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public List<ProductUser> ProductUsers { get; set; }
        public List<UserDiscountCode> UserDiscountCodes { get; set; }

    }
}
