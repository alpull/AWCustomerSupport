﻿using AWCustomerSupport.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AWCustomerSupport.Data {

    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
        }

    }

}
