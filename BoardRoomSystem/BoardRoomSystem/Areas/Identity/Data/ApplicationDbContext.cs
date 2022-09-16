using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardRoomSystem.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<MeetingRoom> MeetingRooms { get; set; }
    public DbSet<AreasViewModel> AreasViewModels { get; set; }
    public DbSet<State> States { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //builder.Entity<ApplicationUser>(b =>
        //{
        //    b.HasMany(e => e.Events)
        //    .WithOne(e => e.Usuario)
        //    .HasForeignKey(ur => ur.IdUser)
        //    .IsRequired();
        //});

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstNameUser).HasMaxLength(255);
        builder.Property(u => u.LastNameUser).HasMaxLength(255);
    }
}