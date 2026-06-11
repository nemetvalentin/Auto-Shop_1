using Microsoft.EntityFrameworkCore;
using WebShop.DAL.Models;

namespace WebShop.DAL
{
    public partial class AutoShopDbContext : DbContext
    {
        public AutoShopDbContext()
        {
        }

        public AutoShopDbContext(DbContextOptions<AutoShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //insert testnih podataka (seed)
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@test.com",
                    Password = "admin123",
                    RoleId = 1,
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    Email = "user@test.com",
                    Password = "user123",
                    RoleId = 2,
                    IsActive = true
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Sedani", Code = "CAT-001", Description = "Putničke limuzine i sedan automobili." },
                new Category { Id = 2, Name = "SUV i terenski", Code = "CAT-002", Description = "Sportsko-terenska vozila i četvorotokaši." },
                new Category { Id = 3, Name = "Sportski automobili", Code = "CAT-003", Description = "Sportski i super-sports automobili visokih performansi." },
                new Category { Id = 4, Name = "Karavan i minivan", Code = "CAT-004", Description = "Karavan vozila, minivani i kombi vozila." }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "BMW Serija 5 520d", Code = "PROD-001", Description = "Elegantna limuzina, 2.0L dizel, 190KS, automatski menjač, kamera za parkiranje.", CategoryId = 1, Price = 52000 },
                new Product { Id = 2, Name = "Toyota Camry 2.5 Hybrid", Code = "PROD-002", Description = "Hibridni pogon, 218KS, premium oprema, Apple CarPlay, adaptive cruise control.", CategoryId = 1, Price = 38500 },
                new Product { Id = 3, Name = "Mercedes-Benz GLE 350d", Code = "PROD-003", Description = "Luksuzni SUV, 3.0L dizel, 272KS, 4MATIC, panoramski krov, 7 sedišta.", CategoryId = 2, Price = 75000 },
                new Product { Id = 4, Name = "Volkswagen Tiguan 2.0 TDI", Code = "PROD-004", Description = "Popularni porodični SUV, 150KS, DSG menjač, Digital Cockpit, LED svetla.", CategoryId = 2, Price = 42000 },
                new Product { Id = 5, Name = "Porsche 911 Carrera 4S", Code = "PROD-005", Description = "Kultni sportski automobil, 3.0L biturbo, 450KS, PDK, Sport Chrono paket.", CategoryId = 3, Price = 135000 },
                new Product { Id = 6, Name = "BMW M4 Competition", Code = "PROD-006", Description = "Sportski coupe, 3.0L biturbo, 510KS, M xDrive, Track mode, karbonski krov.", CategoryId = 3, Price = 89000 },
                new Product { Id = 7, Name = "Ford Focus Karavan 1.5 EcoBlue", Code = "PROD-007", Description = "Praktičan karavan, 120KS, veliki prtljažnik 608L, Android Auto, sigurnosni sistemi.", CategoryId = 4, Price = 28500 },
                new Product { Id = 8, Name = "Renault Espace 1.3 TCe", Code = "PROD-008", Description = "Moderan 7-sedišni minivan, 158KS, EDC menjač, panoramski krov, Google sistemi.", CategoryId = 4, Price = 45000 },
                new Product { Id = 9, Name = "Audi A6 45 TFSI", Code = "PROD-009", Description = "Prestižna limuzina, 2.0L turbo, 245KS, quattro, Matrix LED, virtualna tabla.", CategoryId = 1, Price = 65000 },
                new Product { Id = 10, Name = "Tesla Model 3 Long Range", Code = "PROD-010", Description = "Električni automobil, domet 602km, Autopilot, 0-100 km/h za 4.4s, AWD.", CategoryId = 1, Price = 48000 }
            );

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .Ignore(oi => oi.TotalPrice);
        }
    }
}
