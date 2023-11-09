using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItsLib.DAL.Data
{
    [PrimaryKey(nameof(DiscountCodeId), nameof(UserId))]
    public class UserDiscountCode
    {
        [ForeignKey("DiscountCode")]
        public Guid DiscountCodeId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsUsed { get; set; }
        public User User { get; set; }
        public DiscountCode DiscountCode { get; set; }
    }
}
