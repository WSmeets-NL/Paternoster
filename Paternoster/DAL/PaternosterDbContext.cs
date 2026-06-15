using Microsoft.EntityFrameworkCore;
using Paternoster.Models;
using System.Collections.Generic;

namespace Paternoster.DAL
{
    public class PaternosterDbContext : DbContext
    {
        public DbSet<PaternosterSystem> PaternosterSystems { get; set; }

        public DbSet<Models.Paternoster> Paternosters { get; set; }

        public DbSet<PaternosterContainer> PaternosterContainers { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<ProductPart> ProductParts { get; set; }

        public DbSet<Product>   Products { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<Order> Orders { get; set; }

        public PaternosterDbContext(DbContextOptions<PaternosterDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Paternoster>()
                .HasOne(p => p.PaternosterSystem)
                .WithMany(s => s.Paternosters)
                .HasForeignKey(p => p.PaternosterSystemId);

            modelBuilder.Entity<PaternosterContainer>()
                .HasOne(c => c.Paternoster)
                .WithMany(p => p.Containers)
                .HasForeignKey(c => c.PaternosterId);

            modelBuilder.Entity<Part>()
                .HasOne(p => p.Container)
                .WithOne(c => c.Part)
                .HasForeignKey<Part>(p => p.ContainerId)
                .IsRequired();

            modelBuilder.Entity<ProductPart>()
                .HasOne(pp => pp.Part)
                .WithMany(p => p.Products)
                .HasForeignKey(pp => pp.PartId);

            modelBuilder.Entity<ProductPart>()
                .HasOne(pp => pp.Product)
                .WithMany(pr => pr.ProductParts)
                .HasForeignKey(pp => pp.ProductId);

            modelBuilder.Entity<OrderLine>()
                .HasOne(pr => pr.Product)
                .WithMany(ol => ol.Orders)
                .HasForeignKey(pr => pr.ProductId);

            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(ol => ol.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
        }
    }
}
