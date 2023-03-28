using GraphQL;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SimplestGraphQL.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                   {
                      products {
                        name,
                        price
                       }
                   }
                  "
            };

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync("https://localhost:7285/graphql", new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response: {0}", result);
        }
    }
}