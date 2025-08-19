using Microsoft.EntityFrameworkCore;
using TrainingPieShop.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddDbContext<ShopDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
});
var app = builder.Build();
DbInitializer.Seed(app);
//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
