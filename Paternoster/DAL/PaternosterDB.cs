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

        public TodoDbContext(DbContextOptions<PaternosterDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Paternoster>()
                .HasOne(s => s.PaternosterSystem)
                .WithMany(p => p.Paternosters)
                .HasForeignKey(p => p.PaternosterSystemId);

            modelBuilder.Entity<PaternosterContainer>()
                .HasOne(p => p.Paternoster)
                .WithMany(c => c.Containers)
                .HasForeignKey(c => c.PaternosterId);

            modelBuilder.Entity<Part>()
                .HasOne(c => c.Container)
                .WithOne(p => p.Part);

            modelBuilder.Entity<ProductPart>()
                .HasOne(pp => pp.Part)
                .WithMany(pr => pr.Products)
                .HasForeignKey(pr => pr.ProductId);

            modelBuilder.Entity<ProductPart>()
                .HasOne(pp => pp.Product)
                .WithMany(pr => pr.ProductParts)
                .HasForeignKey(pr => pr.ProductId);

            modelBuilder.Entity<OrderLine>()
                .HasOne(pr => pr.Product)
                .WithMany(ol => ol.Orders)
                .HasForeignKey(pr => pr.ProductId);

            modelBuilder.Entity<OrderLine>()
                .HasOne(o => o.Order)
                .WithMany(ol => ol.OrderLines)
                .HasForeignKey(ol => ol.OrderId);


        }

    }
}
