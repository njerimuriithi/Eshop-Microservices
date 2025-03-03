﻿
namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
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
