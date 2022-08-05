using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoardRoomSystem.Data
{
    public class BoardRoomSystemDBContext : IdentityDbContext
    {
        public BoardRoomSystemDBContext(DbContextOptions<BoardRoomSystemDBContext> options)
            : base(options)
        {
        }

        public DbSet<MeetingRooms> MeetingRooms { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<AreasViewModel> AreasViewModels { get; set; }
        public DbSet<Buildings> Buildings { get; set; }
        public DbSet<AplicationUser> AplicationUsers { get; set; }
        public DbSet<Location> Locations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
