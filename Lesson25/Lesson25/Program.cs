using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using Lesson25.Filters;
using BusinessLogic.Service;
using BusinessLogic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddScoped<IProductService, ProductService>();
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<ShopDbContext>(options =>
    {
        options.UseInMemoryDatabase("ProductDbContext");
    });
}
