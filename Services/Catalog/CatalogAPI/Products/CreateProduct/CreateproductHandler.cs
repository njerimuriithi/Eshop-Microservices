

namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    //Add validation using fluent validation
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must be Greater Than 0");

        }  
    }
    internal class CreateproductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Business logic to create a product        
        

            //Create a new product entity
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,    
                Description = command.Description,  
                ImageFile = command.ImageFile,  
                Price = command.Price,  
            };
        //save to database
        session.Store(product); 
            await session.SaveChangesAsync(cancellationToken);

        //return result
        return new CreateProductResult(product.Id); 


           
        }
    }
}
