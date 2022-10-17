using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Core.Repositories;
using BoardRoomSystem.Repositorio;
using BoardRoomSystem.Repositories;
using BoardRoomSystem.Core;
using BoardRoomSystem.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddSignalR();
//Requerimientos de contraseñas 
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;

})

    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authorization

AddAuthorizationPolicies();

#endregion

AddScoped();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapHub<EventHub>("/eventHub");
app.Run();


void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Admin));
        options.AddPolicy(Constants.Policies.RequireUser, policy => policy.RequireRole(Constants.Roles.User));
    });
}

void AddScoped()
{
    builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
    builder.Services.AddScoped<IRoleRepositorio, RoleRepositorio>();
    builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        options.Cookie.Name = "YourAppCookieName";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromSeconds(600);
        options.LoginPath = "/Identity/Account/Login";
        // ReturnUrlParameter requires 
        //using Microsoft.AspNetCore.Authentication.Cookies;
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.SlidingExpiration = true;
    });
}
