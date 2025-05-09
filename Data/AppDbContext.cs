﻿using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoLinea> CarritoLineas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Carritos)
                .HasForeignKey(c => c.IdUsuario);

            modelBuilder.Entity<CarritoLinea>()
                .HasOne(cl => cl.Carrito)
                .WithMany(c => c.Lineas)
                .HasForeignKey(cl => cl.CarritoId);

            modelBuilder.Entity<CarritoLinea>()
                .HasOne(cl => cl.Producto)
                .WithMany()
                .HasForeignKey(cl => cl.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }
    }
}
