using MediatR;
using SimplestCQRS.Models;

namespace SimplestCQRS.Features.Commands
{
    public class UpdateProductCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, long>
        {
            public async Task<long> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = Product.Products.Where(a => a.Id == command.Id).FirstOrDefault();

                if (product == null)
                {
                    return default;
                }
                else
                {
                    product.UpdatePrie(command.Price);
                    product.Description = command.Description;

                    return await Task.Run(() => product.Id);
                }
            }
        }
    }
}
