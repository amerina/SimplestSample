using GraphQL;
using GraphQL.Builders;
using GraphQL.Types;
using SimplestGraphQL.Context;
using SimplestGraphQL.GraphQL.Types;
using SimplestGraphQL.Models;

namespace SimplestGraphQL.GraphQL.Queries
{
    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery(ProductDbContext db)
        {
            // 查询所有产品
            FieldBuilder<ProductType, List<Product>>
               .Create(typeof(ProductType), "products")
               .Resolve(context =>
               {
                   return db.Products.ToList();
               });

            // 根据Id查询单个产品
            FieldBuilder<ProductType, Product>
             .Create(typeof(ProductType), "product")
             .Arguments(new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                 ))
             .Resolve(context =>
             {
                 var id = context.GetArgument<int>("id");
                 return db.Products.FirstOrDefault(p => p.Id == id);
             });
        }
    }
}
