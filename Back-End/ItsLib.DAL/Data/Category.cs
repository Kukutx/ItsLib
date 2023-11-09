using Microsoft.EntityFrameworkCore;

namespace ItsLib.DAL.Data
{
    [PrimaryKey(nameof(CategoryId))]
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public string Img { get; set; }
        public List<Product> Products { get; set; }
    }
}
