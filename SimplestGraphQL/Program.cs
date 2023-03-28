using GraphQL;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using Microsoft.EntityFrameworkCore;
using SimplestGraphQL.Context;
using SimplestGraphQL.GraphQL;

namespace SimplestGraphQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ProductDbContext>(options => options.UseInMemoryDatabase("Product"));

            //使用最新的GraphQL7.3版本
            builder.Services
                .AddScoped<ProductSchema>();

            builder.Services.AddGraphQL(configure =>
            {
                configure.ConfigureExecutionOptions(options =>
                {
                    options.EnableMetrics = true;
                    options.ThrowOnUnhandledException = true;

                    //var logger = options.RequestServices?.GetRequiredService<ILogger<Program>>();
                    //options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occured", ctx.OriginalException.Message);
                })
                .AddGraphTypes(typeof(ProductSchema).Assembly)
                .AddSystemTextJson();
            });
           

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // 启用GraphiQL界面
            app.UseGraphQLGraphiQL(options: new GraphiQLOptions());

            //add altair UI to development only
            app.UseGraphQLAltair();

            app.UseGraphQLPlayground(options: new PlaygroundOptions());

            app.UseGraphQL<ProductSchema>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}