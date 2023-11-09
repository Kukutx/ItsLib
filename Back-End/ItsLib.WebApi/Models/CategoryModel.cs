using ItsLib.DAL.Data;

namespace ItsLib.WebApi.Models
{
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public List<Product> Products { get; set; }
    }

    public class PostCategoryModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public string Img { get; set; }
    }
}
