using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IntexII.Data;
using IntexII.Models;

var builder = WebApplication.CreateBuilder(args);

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


builder.Services.AddRazorPages();

//adding the ability for sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//additional service for the session, via book 
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

//actually using the session ability 
app.UseSession();

app.UseRouting();

app.UseAuthorization();

//order matters, it will take the first one without looking at the second
//app.MapControllerRoute("pagenumandtype", "{productType}/{pageNum}", new { Controller = "Home", action = "Index" });
//app.MapControllerRoute("pagination", "{pageNum}", new {Controller = "Home", action ="Index", pageNum = 1});
//app.MapControllerRoute("productType", "{productType}", new { Controller = "Home", action = "Index", pageNum = 1 });
app.MapControllerRoute("pagenumandtype", "{productType}/Page{pageNum}", new { Controller = "Home", Action = "Index" });
app.MapControllerRoute("page", "Page/{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });
app.MapControllerRoute("productType", "{productType}", new { Controller = "Home", Action = "Index", pageNum = 1 });
app.MapControllerRoute("pagination", "Products/Page{pageNum}", new { Controller = "Home", Action = "Index", pageNum = 1 });

app.MapDefaultControllerRoute();

app.MapRazorPages();
app.Run();
