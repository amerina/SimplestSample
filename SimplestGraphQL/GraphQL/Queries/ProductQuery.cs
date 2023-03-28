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
            /*
             {
              products {
                name,
                price
               }
              }
             */
            //https://graphql-dotnet.github.io/docs/migrations/migration7/
            // 查询所有产品
            Field<ListGraphType<ProductType>>("products").Resolve(context =>  db.Products.ToList());

            //根据Id查询单个产品
            Field<ProductType>("product")
                .Arguments(new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                 ))
                .Resolve(context => {
                var id = context.GetArgument<int>("id");
               return db.Products.FirstOrDefault(p => p.Id == id);
            });

          
        }
    }
}
