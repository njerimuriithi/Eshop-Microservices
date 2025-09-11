namespace Shopping.Web.Models.Catalog
{
    public class ProductModel
    {
        public Guid Id { get; set; }     
        public string Name { get; private set; } = default!;
        public List<string>Category { get; set; } = new();  
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; private set; } = default!;
        public  string Description { get; set; } = default!;
    }
    //wrapper class for product model
    public record GetProductResponse(IEnumerable<ProductModel> Products);
    public record GetProductByCategoryResponse(IEnumerable<ProductModel> Products);
    public record GetProductByIdResponse(ProductModel Product);

}
