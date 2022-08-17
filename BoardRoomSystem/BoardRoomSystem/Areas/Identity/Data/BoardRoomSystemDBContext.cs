using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace BoardRoomSystem.Data
{
    public class BoardRoomSystemDBContext : IdentityDbContext<ApplicationUser>
    {
        public BoardRoomSystemDBContext(DbContextOptions<BoardRoomSystemDBContext> options)
            : base(options)
        {
        }
        public BoardRoomSystemDBContext()
        {
        }

        public DbSet<MeetingRooms> MeetingRooms { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<AreasViewModel> AreasViewModels { get; set; }
        public DbSet<ApplicationUser> AplicationUsers { get; set; }
        public DbSet<Location> Locations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("BoardRoomSystemDBContextConnection");
                optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
            }
        }



    }
}
