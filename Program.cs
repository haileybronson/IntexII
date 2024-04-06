using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IntexII.Data;
using IntexII.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
//Data 
builder.Services.AddDbContext<ProductDBContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:ProductConnection"]);
});

builder.Services.AddScoped<IProductRepository, EFProductRepository>();

builder.Services.AddRazorPages();

//adding the ability for sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

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
app.MapControllerRoute("pagenumandtype", "{productType}/{pageNum}", new { Controller = "Home", action = "Index" });
app.MapControllerRoute("pagination", "{pageNum}", new {Controller = "Home", action ="Index", pageNum = 1});
app.MapControllerRoute("productType", "{productType}", new { Controller = "Home", action = "Index", pageNum = 1 });

app.MapDefaultControllerRoute();

app.MapRazorPages();
app.Run();
