namespace ItsLib.WebApi.Models
{
    /*
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public double IntroductoryPrice { get; set; }
        public DateTime DateAdded { get; set; }
        public string AdditionalData { get; set; }
        public bool IsDisabled { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductUser> ProductUserModels { get; set; }

        //   public List<ProductUser> ProductUserModels { get; set; }
    }
    */

    public class PostProductModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public double IntroductoryPrice { get; set; }
        public string AdditionalData { get; set; }
    }
}
