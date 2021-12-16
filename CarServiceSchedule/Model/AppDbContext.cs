using Microsoft.EntityFrameworkCore;

namespace CarServiceSchedule.Model
{
    public class AppDbContext : DbContext
    {



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)//migration icin entity framework core tools lazım
        {
            modelBuilder.Entity<CarFeature>()
            .HasIndex(p => new { p.Model, p.Engine, p.InfotainmentSystem, p.InteriorDesign }).IsUnique();

        //     modelBuilder.Entity<Customer>().HasData(
        //   new Customer
        //   {
        //       Id = 1,
        //       Name = "Furkan",
        //       Age = 29
        //   },
        //   new Customer
        //   {
        //       Id = 2,
        //       Name = "Kubra",
        //       Age = 26
        //   }
        //   );

        }

        // public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CarFeature> CarFeatures { get; set; }

    }
}