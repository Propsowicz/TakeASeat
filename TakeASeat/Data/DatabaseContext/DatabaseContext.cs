﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new EventTypeSeed());
            builder.ApplyConfiguration(new EventTagSeed());
            builder.ApplyConfiguration(new EventSeed());
            builder.ApplyConfiguration(new EventTagEventM2MSeed());
        }

    }
}
