using System;
using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BoardRoomSystem.Areas.Identity.IdentityHostingStartup))]
namespace BoardRoomSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BoardRoomSystemDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BoardRoomSystemDBContextConnection")));

                services.AddDefaultIdentity<AplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<BoardRoomSystemDBContext>();
            });
        }
    }
}