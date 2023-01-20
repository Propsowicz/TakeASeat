using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data.DatabaseContext.Seeds;

namespace TakeASeat.Data.DatabaseContext
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions option) : base (option) 
        {
        }

        // set database tables
        public DbSet<Event> Events { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<EventTag> EventTags { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventTagEventM2M> EventTagEventM2M { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<SeatReservation> SeatReservation { get; set; }
        public DbSet<PaymentTransaction> PaymentTransaction { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(b =>
            {
                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
                b.Property(u => u.FirstName).HasMaxLength(250);
                b.Property(u => u.LastName).HasMaxLength(250);
            });

            builder.Entity<Role>(b =>
            {
                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();
            });

            builder.Entity<Event>(b =>
            {
                b.Property(e => e.Name).HasMaxLength(250);
                b.Property(e => e.Description).HasMaxLength(250);
                b.Property(e => e.ImageUrl).HasMaxLength(400);
                b.Property(e => e.Place).HasMaxLength(250);
                b.Property(e => e.EventSlug).HasMaxLength(250);
            });

            builder.Entity<EventTag>(b =>
            {
                b.Property(et => et.TagName).HasMaxLength(250);
            });

            builder.Entity<EventType>(b =>
            {
                b.Property(et => et.Name).HasMaxLength(250);
            });

            builder.Entity<PaymentTransaction>(b =>
            {
                b.Property(pt => pt.Currency).HasMaxLength(3);
                b.Property(pt => pt.Description).HasMaxLength(250);
            });                       

            builder.Entity<Seat>(b =>
            {
                b.Property(s => s.Row).HasMaxLength(1);
                b.Property(s => s.SeatColor).HasMaxLength(20);
                b.HasOne(s => s.Show).WithMany(s => s.Seats).HasForeignKey(s => s.ShowId).OnDelete(DeleteBehavior.Cascade);
                b.HasOne(s => s.SeatReservation).WithMany(sr => sr.Seats).HasForeignKey(s => s.ReservationId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<SeatReservation>(b =>
            {                
                b.HasOne(sr => sr.User).WithMany().HasForeignKey(sr => sr.UserId).OnDelete(DeleteBehavior.Cascade);
                b.HasOne(sr => sr.PaymentTransaction).WithMany(pt => pt.SeatReservations)
                                                    .HasForeignKey(sr => sr.PaymentTransactionId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Show>(b =>
            {
                b.Property(s => s.Description).HasMaxLength(250);
            });

            builder.ApplyConfiguration(new UserSeed());
            builder.ApplyConfiguration(new EventTypeSeed());
            builder.ApplyConfiguration(new EventTagSeed());
            builder.ApplyConfiguration(new EventSeed());
            builder.ApplyConfiguration(new EventTagEventM2MSeed());
            builder.ApplyConfiguration(new ShowSeed());
            builder.ApplyConfiguration(new UserRoleSeed());
            builder.ApplyConfiguration(new UserRoleM2MSeed());

        }

    }
}
