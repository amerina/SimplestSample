using GraphQL.Types;
using SimplestGraphQL.Models;

namespace SimplestGraphQL.GraphQL.Types
{
    /// <summary>
    /// 创建一个产品类型类，定义产品的属性
    /// </summary>
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Name = "Product";
            Field(x => x.Id).Description("The Id of the product.");
            Field(x => x.Name).Description("The name of the product.");
            Field(x => x.Price).Description("The price of the product.");
        }
    }
}
