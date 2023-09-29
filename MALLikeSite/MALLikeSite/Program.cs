using MALLikeSite.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using MALLikeSite;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using MALLikeSite.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("MALLikeSite")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITitleService, TitleService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
IMvcBuilder mvcBuilder = builder.Services.AddControllersWithViews()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

static void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseInMemoryDatabase("TitleDbContext");
    });

    services.AddScoped<IValidator<CreateTitleModel>, CreateTitleModelValidator>();
    services.AddScoped<IValidator<EditTitleModel>, EditTitleModelValidator>();
    services.AddScoped<IValidator<CreateStaffModel>, CreateStaffModelValidator>();
    services.AddScoped<IValidator<EditStaffModel>, EditStaffModelValidator>();
}