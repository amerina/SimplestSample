using MediatR;
using SimplestCQRS.Models;

namespace SimplestCQRS.Features.Commands
{
    public class DeleteProductByNameCommand : IRequest<string>
    {
        public string Name { get; set; }
        public class DeleteProductByNameCommandHandler : IRequestHandler<DeleteProductByNameCommand, string>
        {
            public async Task<string> Handle(DeleteProductByNameCommand command, CancellationToken cancellationToken)
            {
                var product = Product.Products.Where(a => a.Name == command.Name).FirstOrDefault();
                if (product == null) return null;

                Product.Products.Remove(product);

                return await Task.Run(() => product.Name);
            }
        }
    }
}
