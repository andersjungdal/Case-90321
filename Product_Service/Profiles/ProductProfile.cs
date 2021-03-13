namespace Product_Service.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Product_Service.Db.Product, Product_Service.Models.Product>();
            CreateMap<Product_Service.Models.Product, Product_Service.Db.Product>();
        }
    }
}
