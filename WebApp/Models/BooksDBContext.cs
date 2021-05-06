using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class BooksDBContext : DbContext
    {
        public BooksDBContext() : this(false)
        {

        }

        public BooksDBContext(bool bFromScratch) : base()
        {
            if (bFromScratch)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public BooksDBContext(DbContextOptions<BooksDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BooksDB;Trusted_connection=TRUE");
            }
        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Publisher>().ToTable("Publisher");
        }
    }
}
