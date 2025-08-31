using Pharma.API.Middleware;
using Pharma.API.Services;
using Pharma.Model;
using System.Text.Json.Serialization;

namespace Pharma.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add MemoryCache service
            builder.Services.AddMemoryCache();

            // Add services to the container.
            builder.Services.AddSingleton<IOrderService, OrderService>();
            builder.Services.Configure<ReviewOptions>(builder.Configuration.GetSection("Review"));
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Makes enums serialize/deserialize as strings instead of numbers
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.UseMiddleware<CorrelationIdMiddleware>();

            // Load and cache orders at startup
            using (var scope = app.Services.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                await orderService.CacheOrdersAsync(env.ContentRootPath);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
