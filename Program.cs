using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IntexII.Data;
using IntexII.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.ML.OnnxRuntime;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Configures the HSTS services
builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(30);  // Sets the duration to 30, default
    options.IncludeSubDomains = true;        // This ensures that the HSTS policy is applied to all subdomains
    options.Preload = true;                  // This includes our domain in browsers' preload list so they know to always use HTTPS
});

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
    .AddRoles<IdentityRole>()  // Add this line to include role support
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();



//DB Contexts and Repo Pattern 
builder.Services.AddDbContext<ProductDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
});

builder.Services.AddScoped<IProductRepository, EFProductRepository>();

//usersDBContext
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
        "default-src 'self'; " +
        "font-src 'self' fonts.gstatic.com; " +
        "style-src 'self' fonts.googleapis.com; " +
        "img-src 'self' m.media-amazon.com www.lego.com images.brickset.com www.brickeconomy.com");
    await next();
});

// Use HSTS middleware
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//actually using the session ability 
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//order matters, most specific should come first

//app.MapControllerRoute("pagenumandproducttype", "{productType}/Page{pageNum}", new { Controller = "Home", Action = "Products" });
//app.MapControllerRoute("pagenumandcolortype", "{colorType}/Page{pageNum}", new { Controller = "Home", Action = "Products" });
//app.MapControllerRoute("productType", "{productType}", new { Controller = "Home", Action = "Products", pageNum = 1 });
//app.MapControllerRoute("colorType", "{colorType}", new { Controller = "Home", Action = "Products", pageNum = 1 });

app.MapControllerRoute("products","Products/{pageNum}", new {Controller = "Home", action = "Products", pageNum = 1});
app.MapControllerRoute("productDetail", "ProductDetail/{productId}", new {Controller = "Home", action = "ProductDetail"});

//name: "productsWithColor",
// pattern: "Products/{productId}/{colorType}",
// defaults: new { Controller = "Home", Action = "ProductDetails" }

app.MapDefaultControllerRoute();


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRolesAsync(userManager, roleManager);
    //await SeedAdminUserAsync(userManager, roleManager);
}
/*
async Task SeedAdminUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    string adminEmail = "admin@email.com"; // Use a secure way to store and retrieve this
    string adminPassword = "GroupPiIntex314!"; // Use a secure way to store and retrieve this
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser()
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true // Confirm email to bypass email verification
        };
        var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
        if (createUserResult.Succeeded)
        {
            // Check if the admin role exists
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }
            // Add the admin user to the admin role
            var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!addToRoleResult.Succeeded)
            {
                // Handle the case where the admin user could not be added to the admin role
                throw new InvalidOperationException("Failed to add user to Admin role.");
            }
        }
        else
        {
            // Handle the case where the admin user could not be created
            throw new InvalidOperationException("Failed to create the Admin user.");
        }
        if (createUserResult.Succeeded)
        {
            // ...
        }
        else
        {
            var errors = createUserResult.Errors.Select(e => e.Description);
            // Log these errors
            // For example:
            foreach (var error in errors)
            {
                // Use your logger here
                Console.WriteLine(error); // Or use a proper logging mechanism
            }
            throw new InvalidOperationException($"Failed to create the Admin user: {string.Join(", ", errors)}");
        }
    }
}
*/

async Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        var adminRole = new IdentityRole("Admin");
        await roleManager.CreateAsync(adminRole);
    }
    if (!await roleManager.RoleExistsAsync("User"))
    {
        var userRole = new IdentityRole("User");
        await roleManager.CreateAsync(userRole);
    }
}

app.MapRazorPages();
app.Run();


