﻿
using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) :ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ProductID is Required");
            RuleFor(x => x.Name).NotEmpty().Length(2,150).WithMessage("Name is Required");
         
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must be Greater Than 0");

        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
           var product = await session.LoadAsync<Product>(command.Id,cancellationToken); 
           if(product is null)
            {
                throw new productNotFoundException();
            }
            product.Name = command.Name;
            product.Category = command.Category; 
            product.Description = command.Description;  
            product.Price = command.Price;  
            product.ImageFile= command.ImageFile;

            session.Update(product);    
            
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);

        }
    }
}
