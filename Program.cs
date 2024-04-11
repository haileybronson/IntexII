using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IntexII.Data;
using IntexII.Models;
using Microsoft.ML.OnnxRuntime;

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

//usersDBContext
builder.Services.AddDbContext<UsersDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});
//add repo pattern

//ordersDBcontext
builder.Services.AddDbContext<OrdersDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});
//add orders repopattern
builder.Services.AddDbContext<CrudDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});

//LineItemsDBContext
builder.Services.AddDbContext<LineItemsDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});

//add lineitems repo pattern

//customersDBContext
builder.Services.AddDbContext<CustomersDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});
//add customers repo pattern

//Admins DB Context
builder.Services.AddDbContext<AdminsDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});
//add admins repo pattern

//builder.Services.AddScoped<EFCrudURepository>();
// Registering EFCrudRepository
builder.Services.AddScoped<EFCrudRepository>();

// Registering ICrudRepository<Product> with EFCrudRepository
builder.Services.AddScoped<ICrudRepository<Product>, EFCrudRepository>();
//builder.Services.AddScoped<ICrudURepository<AspNetUsers>, EFCrudURepository>();

// Registering DbContexts for dependency injection
builder.Services.AddScoped<CrudDBContext>();
//builder.Services.AddScoped<CrudUDBContext>();

builder.Services.AddRazorPages();

//adding the ability for sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//additional service for the session, via book 
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddSingleton<InferenceSession>(sp => 
{
    // Make sure to handle the path to the ONNX model file correctly.
    return new InferenceSession("decision_tree_model (1).onnx");
});


var app = builder.Build();

//Content-Security-Policy header
app.Use(async (ctx, next) =>
{
    ctx.Response.Headers.Add("Content-Security-Policy",
        "default-src 'self'");
    await next();
});

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

//actually using the session ability 
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//order matters, it will take the first one without looking at the second

//app.MapControllerRoute("pagenumandproducttype", "{productType}/Page{pageNum}", new { Controller = "Home", Action = "Products" });
//app.MapControllerRoute("pagenumandcolortype", "{colorType}/Page{pageNum}", new { Controller = "Home", Action = "Products" });
//app.MapControllerRoute("page", "Page/{pageNum}", new { Controller = "Home", Action = "Products", pageNum = 1 });
//app.MapControllerRoute("productType", "{productType}", new { Controller = "Home", Action = "Products", pageNum = 1 });
//app.MapControllerRoute("colorType", "{colorType}", new { Controller = "Home", Action = "Products", pageNum = 1 });
//app.MapControllerRoute("pagination", "Products/Page{pageNum}", new { Controller = "Home", Action = "Products", pageNum = 1 });


app.MapControllerRoute("products","Products/Page{pageNum}", new { Controller = "Home", Action = "Products", pageNum = 1 });
app.MapControllerRoute("productDetail", "Home/ProductDetail/{productId}", new { Controller = "Home", Action = "ProductDetail" });

app.MapDefaultControllerRoute();

app.MapRazorPages();
app.Run();


