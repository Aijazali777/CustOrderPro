using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustOrderPro.Models
{
    public class AppDbContext : IdentityDbContext<Customer>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<CryptoOrder> CryptoOrders { get; set; }
        public DbSet<OrderReceipt> OrderReceipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().HasKey(e => e.OrderId);
            modelBuilder.Entity<CryptoOrder>().HasKey(e => e.CryptoId);
            modelBuilder.Entity<OrderReceipt>().HasKey(e => e.OrderReceiptId);

            // One to many relationship 
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(e => e.Customers)
                .WithMany(e => e.Orders).HasForeignKey(e => e.CustomerId).IsRequired();
            });

            // One to One relationship
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(e => e.OReceipt)
                .WithOne(e => e.POrder).HasForeignKey<OrderReceipt>(e => e.OrderId).IsRequired();
            });

            // One to many relationship
            modelBuilder.Entity<CryptoOrder>(entity =>
            {
                entity.HasOne(e => e.Customers)
                .WithMany(e => e.CryptoOrders).HasForeignKey(e => e.CustomerId).IsRequired();
            });

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = "1",
                    Name = "John",
                    Email ="john@custorderpro.com",
                    Gender = "Male",
                    PhoneNumber = "5555551234",
                    Address = "Street 1",
                    City = "New York",
                    Country = "United States"
                },
                new Customer
                {
                    Id = "2",
                    Name = "Smith",
                    Email = "smith@custorderpro.com",
                    Gender = "Male",
                    PhoneNumber = "5555551235",
                    Address = "Street 2",
                    City = "Dallas",
                    Country = "United Kingdom"
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    CustomerId = "ebcdbc68-6b67-49e8-8ced-253284b27b3d",
                    NumberOfItems = 6,
                    TotalPrice = 500,
                    PaymentStatus = "Paid"
                }
            );

            modelBuilder.Entity<OrderReceipt>().HasData(
                new OrderReceipt
                {
                    OrderReceiptId = 1,
                    OrderId = 1,
                    ReceiptDetails = "#CustOrderPro Order Receipt#\tCustomerName: John, Total Items: 6, Total Price: 500, Bill: Paid."
                }
            );
        }
    }
}
