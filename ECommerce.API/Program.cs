using ECommerce.API.Extensions;
using ECommerce.Application;
using ECommerce.Application.Common;
using ECommerce.Application.MappingProfiles;
using ECommerce.Infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection("UrlSettings"));


builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();



await app.SeedAndMigrateDataAsync();



app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")),
    RequestPath = "/Files"
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
