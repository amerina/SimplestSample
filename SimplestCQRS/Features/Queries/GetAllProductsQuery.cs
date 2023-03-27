using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SimplestCQRS.Models;
using System.Text;

namespace SimplestCQRS.Features.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
        {
            public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
            {
                return await Task.Run(() => Product.Products);
            }
        }
    }
}
