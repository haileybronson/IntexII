using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IntexII.Data;
using IntexII.Models;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


services.AddAuthentication().AddGoogle(googleOptions =>
{
   googleOptions.ClientId = configuration["Authentication_Google_ClientId"];
   googleOptions.ClientSecret = configuration["Authentication_Google_ClientSecret"];
});


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ProductConnection") ?? throw new InvalidOperationException("Connection string 'ProductConnection' not found.");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


//DB Contexts and Repo Pattern
builder.Services.AddDbContext<ProductDBContext>(options =>
{
   options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});


builder.Services.AddScoped<IProductRepository, EFProductRepository>();


// Registering DB Contexts for other entities
builder.Services.AddDbContext<UsersDBContext>(options =>
{
   options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});
builder.Services.AddDbContext<OrdersDBContext>(options =>
{
   options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});
builder.Services.AddDbContext<LineItemsDBContext>(options =>
{
   options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});
builder.Services.AddDbContext<CustomersDBContext>(options =>
{
   options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});
builder.Services.AddDbContext<AdminsDBContext>(options =>
{
   options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});


// Registering CrudDBContext
builder.Services.AddDbContext<CrudDBContext>(options =>
{
   options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});

builder.Services.AddDbContext<CrudUDBContext>(options =>
{
   options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});

builder.Services.AddScoped<EFCrudURepository>();
// Registering EFCrudRepository
builder.Services.AddScoped<EFCrudRepository>();


// Registering ICrudRepository<Product> with EFCrudRepository
builder.Services.AddScoped<ICrudRepository<Product>, EFCrudRepository>();
builder.Services.AddScoped<ICrudURepository<AspNetUsers>, EFCrudURepository>();


// Registering DbContexts for dependency injection
builder.Services.AddScoped<CrudDBContext>();
builder.Services.AddScoped<CrudUDBContext>();


builder.Services.AddRazorPages();


// Adding session capabilities
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


// Additional service for session management
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

app.UseRouting();

// Configuring the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
   app.UseMigrationsEndPoint();
}
else
{
   app.UseExceptionHandler("/Home/Error");
   app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


// Adding controller routes
app.MapControllerRoute("pagenumandtype", "{productType}/Page{pageNum}", new { Controller = "Home", Action = "Index" });
app.MapControllerRoute("page", "Page/{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });
app.MapControllerRoute("productType", "{productType}", new { Controller = "Home", Action = "Index", pageNum = 1 });
app.MapControllerRoute("pagination", "Products/Page{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });

app.MapDefaultControllerRoute();
app.MapRazorPages();


app.Run();