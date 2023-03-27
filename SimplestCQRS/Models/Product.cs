namespace SimplestCQRS.Models
{
    public class Product
    {
        public static List<Product> Products = new List<Product>
        {
            new Product("重构",".NET经典丛书系列",88.50M)
            {
                Id = 1
            },
            new Product("代码大全",".NET经典丛书系列",188.50M)
            {
                Id = 2
            },
            new Product("微服务架构",".NET经典丛书系列",188.50M) { Id = 3 },
        };
        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public long Id { get; set; }
        public string Name { get; private set; }
        public string? Description { get; set; }
        public decimal Price { get; private set; }

        public void UpdatePrie(decimal price)
        {
            this.Price = price;
        }
    }
}
