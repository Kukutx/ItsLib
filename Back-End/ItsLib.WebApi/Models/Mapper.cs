using ItsLib.DAL.Data;

namespace ItsLib.WebApi.Models
{
    public class Mapper
    {
        public DiscountCodeModel MapEntityToModel(DiscountCode entity)
        {
            DiscountCodeModel model = new DiscountCodeModel();
            model.DiscountCodeId = entity.DiscountCodeId;
            model.Code = entity.Code;
            model.IsDisabled = entity.IsDisabled;
            model.Discount = entity.Discount;
            return model;
        }

        public UserDiscountCodeModel MapEntityToModel(UserDiscountCode entity)
        {
            UserDiscountCodeModel model = new UserDiscountCodeModel();
            model.UserId = entity.UserId;
            model.User = MapEntityToModel(entity.User);
            model.IsDisabled = entity.IsDisabled;
            model.DiscountCodeId = entity.DiscountCodeId;
            model.DiscountCode = entity.DiscountCode;
            return model;
        }


        public ProductUserModel MapEntityToModel(ProductUser entity)
        {
            ProductUserModel model = new ProductUserModel();
            model.ProductId = entity.ProductId;
            model.UserId = entity.UserId;
            model.User = MapEntityToModel(entity.User);
            model.Product = entity.Product;
            model.LastModifiedDate = entity.LastModifiedDate;
            model.DateAdded = entity.DateAdded;
            model.IsDisabled = entity.IsDisabled;
            model.Review = entity.Review;
            model.ReviewTitle = entity.ReviewTitle;
            model.IsUsed = entity.IsUsed;
            model.InWishList = entity.InWishList;
            return model;
        }

        public UserModel MapEntityToModel(User entity)
        {
            UserModel model = new UserModel();
            model.Id = entity.Id;
            model.LoyaltyCardCode = entity.LoyaltyCardCode;
            model.Name = entity.Name;
            model.Surname = entity.Surname;
            model.UserName = entity.UserName;
            model.IsDisabled = entity.IsDisabled;
            return model;
        }

        public CategoryModel MapEntityToModel(Category entity)
        {
            CategoryModel model = new CategoryModel();
            model.CategoryId = entity.CategoryId;
            model.Name = entity.Name;
            model.IsDisabled = entity.IsDisabled;
            return model;
        }

        public Category MapPostCategory(PostCategoryModel model)
        {
            Category entity = new Category();
            entity.CategoryId = Guid.NewGuid();
            entity.Name = model.Name;
            entity.IsDisabled = false;
            entity.Img = model.Img;
            entity.Color = model.Color;
            entity.Icon = model.Icon;
            return entity;
        }

        public Product MapPostProduct(PostProductModel model)
        {
            Product entity = new Product();
            entity.ProductId = Guid.NewGuid();
            entity.CategoryId = model.CategoryId;
            entity.Name = model.Name;
            entity.IntroductoryPrice = model.IntroductoryPrice;
            entity.AdditionalData = model.AdditionalData;
            entity.IsDisabled = false;
            return entity;
        }

        public ProductUser MapPostInWhisList(PostInWhisList model, string id)
        {
            ProductUser entity = new ProductUser();
            entity.ProductId = model.ProductId;
            entity.UserId = id;
            entity.InWishList = true;
            entity.IsUsed = false;
            entity.IsDisabled = false;
            entity.LastModifiedDate = DateTime.Now;
            entity.DateAdded = DateTime.Now;
            return entity;
        }

        public ProductUser MapPostIsUsed(PostIsUsed model, string id)
        {
            ProductUser entity = new ProductUser();
            entity.ProductId = model.ProductId;
            entity.InWishList = false;
            entity.IsUsed = true;
            entity.IsDisabled = false;
            entity.UserId = id;
            entity.DateAdded = DateTime.Now;
            entity.LastModifiedDate = DateTime.Now;
            return entity;
        }

        public DiscountCode MapPostDiscountCode(int discount, string code)
        {
            DiscountCode entity = new DiscountCode();
            entity.DiscountCodeId = Guid.NewGuid();
            entity.IsDisabled = false;
            entity.Discount = discount;
            entity.Code = code;
            return entity;
        }

        public UserDiscountCode MapPostUserDiscountCode(PostUserDiscountCodeModel model)
        {
            UserDiscountCode entity = new UserDiscountCode();
            entity.UserId = model.UserId;
            entity.IsDisabled = false;
            entity.DiscountCodeId = model.DiscountCodeId;
            return entity;
        }

        /*
     
        public Category MapModelToEntity(CategoryModel model)
        {
            Category entity = new Category();
            entity.CategoryId = model.CategoryId;
            entity.Name = model.Name;
            entity.IsDisabled = model.IsDisabled;
            return entity;
        }

        public User MapModelToEntity(UserModel model)
        {
            User entity = new User();
            entity.Id = model.Id;
            entity.LoyaltyCardCode = model.LoyaltyCardCode;
            entity.Name = model.Name;
            entity.Surname = model.Surname;
            entity.UserName = model.UserName;
            entity.IsDisabled = model.IsDisabled;
            return entity;
        }

        public Product MapModelToEntity(ProductModel model)
        {
            Product entity = new Product();
            entity.ProductId = model.ProductId;
            entity.CategoryId = model.CategoryId;
            entity.Name = model.Name;
            entity.IntroductoryPrice = model.IntroductoryPrice;
            entity.AdditionalData = model.AdditionalData;
            entity.IsDisabled = model.IsDisabled;
            entity.Category = model.Category;
            entity.DateAdded = DateTime.Now;
            entity.ProductUsers = model.ProductUserModels;
            return entity;
        }

        public ProductModel MapEntityToModel(Product entity)
        {
            ProductModel model = new ProductModel();
            model.ProductId = entity.ProductId;
            model.CategoryId = entity.CategoryId;
            model.Name = entity.Name;
            model.IntroductoryPrice = entity.IntroductoryPrice;
            model.AdditionalData = entity.AdditionalData;
            model.IsDisabled = entity.IsDisabled;
            model.Category = entity.Category;
            model.ProductUserModels = entity.ProductUsers;
            return model;
        }

        public UserDiscountCode MapModelToEntity(UserDiscountCodeModel model)
        {
            UserDiscountCode entity = new UserDiscountCode();
            entity.UserId = model.UserId;
            entity.DiscountCodeId = model.DiscountCodeId;
            entity.IsDisabled = model.IsDisabled;
            entity.User = MapModelToEntity(model.User);
            entity.DiscountCode = model.DiscountCode;
            return entity;
        }

        public DiscountCode MapModelToEntity(DiscountCodeModel model)
        {
            DiscountCode entity = new DiscountCode();
            entity.DiscountCodeId = model.DiscountCodeId;
            entity.Code = model.Code;
            entity.IsDisabled = model.IsDisabled;
            entity.Discount = model.Discount;
            return entity;
        }

        */

    }
}
