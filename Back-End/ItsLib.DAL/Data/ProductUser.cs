using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItsLib.DAL.Data
{
    [PrimaryKey(nameof(UserId), nameof(ProductId))]
    public class ProductUser
    {
        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public bool InWishList { get; set; }
        public bool IsUsed { get; set; }
        public bool IsDisabled { get; set; }
        public string? Review { get; set; }
        public string? ReviewTitle { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
