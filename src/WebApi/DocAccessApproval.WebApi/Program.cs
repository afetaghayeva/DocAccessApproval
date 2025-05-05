
using DocAccessApproval.Application;
using DocAccessApproval.Application.Extensions;
using DocAccessApproval.Persistence;
using DocAccessApproval.WebApi.Extensions;
using DocAccessApproval.WebApi.Middlewares;

namespace DocAccessApproval.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddApplicationRegistration();
        builder.Services.AddPersistenceRegistration(builder.Configuration);

        builder.Services.AddJwtAuthentication(builder.Configuration);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerWithJwt();

        var app = builder.Build();

        app.Configuration.MigrateAndSeed();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(c =>
            {
                c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
            });
            app.UseSwaggerUI();
        }
        app.ConfigureCustomExceptionMiddleware();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
