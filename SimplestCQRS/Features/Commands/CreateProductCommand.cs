using MediatR;
using SimplestCQRS.Models;

namespace SimplestCQRS.Features.Commands
{
    public class CreateProductCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
        {
            public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product(command.Name, command.Description, command.Price)
                {
                    Id = Product.Products.Count + 1
                };
               
                Product.Products.Add(product);

                return await Task.Run(() => product.Id);
            }
        }
    }
}
