using CarStore.Database.Entities;
using CarStore.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Database
{
    public class AbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-2ORINJ47;Database=CarStore; Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- Category Foreign keys opførsel --- //
            // On delete restrict
            modelBuilder.Entity<Product>()
                .HasOne(lambda => lambda.Category)
                .WithMany(lambda => lambda.Product)
                .OnDelete(DeleteBehavior.Restrict);
            // --- Category Foreign keys opførsel --- //


            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Email = "Ramazan@admin.com",
                Password = "Passw0rd",
                Role = Role.Admin
            },
            new User
            {
                Id = 2,
                Email = "Ramazan@user.com",
                Password = "Passw0rd",
                Role = Role.User
            });

            modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                AddressName = "Telegrafvej 9",
                ZipCode = 2750,
                CityName = "Ballerup",
                UserId = 1,
            },
            new Customer
            {
                Id = 2,
                AddressName = "Karlsgårdsvej 17",
                ZipCode = 3000,
                CityName = "Helsingør",
                UserId = 2,
            });


            modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                OrderDateTime = DateTime.Now,
                UserId = 1
            },
            new Order
            {
                Id = 2,
                OrderDateTime = DateTime.Now,
                UserId = 1
            });

            // Order_Items
            modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem
            {
                Id = 1,
                Price = 7500,
                Quantity = 2,       // Antal af vare købt
                OrderId = 1,    // Id på køberen
                ProductId = 1       // Id på produkt købt
            },
            new OrderItem
            {
                Id = 2,
                Price = 6500,
                Quantity = 22,      // Antal af vare købt
                OrderId = 1,    // Id på køberen
                ProductId = 1       // Id på produkt købt
            });

            // Vores Categorys
            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                CategoryName = "Mclaren"
            },
            new Category
            {
                Id = 2,
                CategoryName = "Mercedes"
            },
            new Category
            {
                Id = 3,
                CategoryName = "Rolls-Royce"
            },
            new Category
            {
                Id = 4,
                CategoryName = "Bugatti"
            }
            );

            // Vores Produkter
            modelBuilder.Entity<Product>().HasData(
            // 
            new Product
            {
                Id = 1,
                ProductName = "McLaren 720s",
                Price = 1000000,
                ProductionYear = 2020,
                Km = 100,
                Description = "Flot Bil",
                CategoryId = 1
            },
            // 
            new Product
            {
                Id = 2,
                ProductName = "McLaren P1",
                Price = 1000000,
                ProductionYear = 2019,
                Km = 0,
                Description = "Flot Bil",
                CategoryId = 1
            },
            // 
            new Product
            {
                Id = 3,
                ProductName = "Mercedes C63S",
                Price = 10000000,
                ProductionYear = 2021,
                Km = 0,
                Description = "Flot Bil",
                CategoryId = 2
            },
            // 
            new Product
            {
                Id = 4,
                ProductName = "Mercedes-AMG GT",
                Price = 1000000,
                ProductionYear = 2018,
                Km = 10000,
                Description = "Flot Bil",
                CategoryId = 2
            },
            // 
            new Product
            {
                Id = 5,
                ProductName = "Rolls-Royce Phantom",
                Price = 10000000,
                ProductionYear = 2021,
                Km = 10,
                Description = "Flot Bil",
                CategoryId = 3
            },
            // 
            new Product
            {
                Id = 6,
                ProductName = "Bugatti Chiron",
                Price = 10000000,
                ProductionYear = 2016,
                Km = 1000,
                Description = "Flot Bil",
                CategoryId = 4
            }
            );
        }
    }
}
