using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItsLib.DAL.Data
{
    [PrimaryKey(nameof(ProductId))]
    public class Product
    {
        public Guid ProductId { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public double IntroductoryPrice { get; set; }
        public DateTime DateAdded { get; set; }
        public string AdditionalData { get; set; }
        public bool IsDisabled { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductUser> ProductUsers { get; set; }
    }
}
