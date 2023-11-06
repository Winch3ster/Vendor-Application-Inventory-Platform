using Microsoft.AspNetCore.Authentication.Cookies;
using Vendor_Application_Inventory_Platform.Data_Access_Layer;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
//using Vendor_Application_Inventory_Platform.Data.Services;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Vendor_Application_Inventory_Platform.Areas.Admin.Data.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddTransient<DatabaseSeeder>();

//Add local database connection
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//Add services
builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Access/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("isAdmin", "true");
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});




app.UseMvc();

app.Run();

