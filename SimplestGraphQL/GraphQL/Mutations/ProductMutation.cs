using GraphQL;
using GraphQL.Builders;
using GraphQL.Types;
using GraphQL.Utilities;
using SimplestGraphQL.Context;
using SimplestGraphQL.GraphQL.Types;
using SimplestGraphQL.Models;
using System.Security.Cryptography;

namespace SimplestGraphQL.GraphQL.Mutations
{
    public class ProductMutation : ObjectGraphType
    {
        public ProductMutation(ProductDbContext db)
        {
            // 创建一个新的产品
            FieldBuilder<ProductType, Product>
                .Create(typeof(ProductType), "createProduct")
                .Description("Create Product.")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<DecimalGraphType>> { Name = "price" }
                 ))
                .Resolve(context =>
                {
                    var product = new Product
                    {
                        Name = context.GetArgument<string>("name"),
                        Price = context.GetArgument<decimal>("price")
                    };
                    db.Products.Add(product);
                    db.SaveChanges();
                    return product;
                });

            // 更新一个已有的产品
            FieldBuilder<ProductType, Product>
               .Create(typeof(ProductType), "updateProduct")
               .Description("Update Product.")
               .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "name" },
                    new QueryArgument<DecimalGraphType> { Name = "price" }
                ))
               .Resolve(context =>
               {
                   var id = context.GetArgument<int>("id");
                   var product = db.Products.FirstOrDefault(p => p.Id == id);
                   if (product == null)
                   {
                       context.Errors.Add(new ExecutionError("Product not found"));
                       return null;
                   }
                   var name = context.GetArgument<string>("name");
                   var price = context.GetArgument<decimal?>("price");
                   if (name != null)
                   {
                       product.Name = name;
                   }
                   if (price != null)
                   {
                       product.Price = price.Value;
                   }
                   db.SaveChanges();
                   return
                     product;
               });

            // 删除一个已有的产品
            FieldBuilder<ProductType, Product>
               .Create(typeof(ProductType), "deleteProduct")
               .Description("Update Product.")
               .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ))
               .Resolve(context =>
               {
                   var id = context.GetArgument<int>("id");
                   var product = db.Products.FirstOrDefault(p => p.Id == id);
                   if (product == null)
                   {
                       context.Errors.Add(new ExecutionError("Product not found"));
                       return null;
                   }
                   db.Products.Remove(product);
                   db.SaveChanges();
                   return product;
               });

        }
    }
}
