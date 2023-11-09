using ItsLib.DAL.Data;

namespace ItsLib.WebApi.Models
{
    public class ProductUserModel
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public bool InWishList { get; set; }
        public bool IsUsed { get; set; }
        public string? Review { get; set; }
        public string? ReviewTitle { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public UserModel User { get; set; }
        public Product Product { get; set; }
    }

    public class PostReview
    {
        public Guid ProductId { get; set; }
        public string Review { get; set; }
        public string ReviewTitle { get; set; }
    }

    public class PostInWhisList
    {
        public Guid ProductId { get; set; }
    }

    public class PostIsUsed
    {
        public Guid ProductId { get; set; }
    }
}
