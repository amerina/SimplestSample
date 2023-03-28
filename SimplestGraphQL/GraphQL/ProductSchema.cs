using GraphQL.Types;
using SimplestGraphQL.GraphQL.Mutations;
using SimplestGraphQL.GraphQL.Queries;

namespace SimplestGraphQL.GraphQL
{
    public class ProductSchema : Schema
    {
        public ProductSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<ProductQuery>();

            Mutation = provider.GetRequiredService<ProductMutation>();
        }
    }
}
