using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using practica1a.Models;

namespace practica1a.Data
{
    public class practicaDbContext : DbContext
    {

        public practicaDbContext(DbContextOptions<practicaDbContext> options) : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Rol> Rols { get; set; }


        public DbSet<UsuarioSession> UsuariosSessions { get; set; }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<DetalleVenta> DetalleVentas { get; set; }

        public DbSet<DetalleCompra> DetalleCompras { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<OrdenCompra> OrdenCompras { get; set; }

        public DbSet<RolUsuario> RolsUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<RolUsuario>().HasKey(ru => new { ru.UsuarioId, ru.RolId });

            builder.Entity<RolUsuario>()
            .HasOne(ru => ru.Usuario)
            .WithMany(b => b.RolsUsuarios)
            .HasForeignKey(bc => bc.UsuarioId);

            builder.Entity<RolUsuario>()
                .HasOne(bc => bc.Rol)
                .WithMany(c => c.RolsUsuarios)
                .HasForeignKey(bc => bc.RolId);


            builder.Entity<Usuario>().HasOne(t => t.UsuariosSession)
                    .WithOne(t => t.Usuarios)
                     .HasForeignKey<UsuarioSession>(t => t.UsuarioId);


            builder.Entity<UsuarioSession>().HasOne(t => t.Usuarios)
                      .WithOne(t => t.UsuariosSession)
                     .HasForeignKey<Usuario>(t => t.UsuarioSessionId);


           // builder.Entity<UsuariosPrueba>().HasOne(t => t.UsuarioSessionActive)
           //           .WithOne(t => t.UsuariosPrueba)
           //             .HasForeignKey<UsuarioSessionActive>(t => t.UsuariosPruebaId);


           //builder.Entity<UsuarioSessionActive>().HasOne(t => t.UsuariosPrueba)
           //          .WithOne(t => t.UsuarioSessionActive)
           //         .HasForeignKey<UsuariosPrueba>(t => t.UsuarioSessionActiveId);

          //  builder.Entity<OrdenCompra>()
          //.HasOne(d => d.Usuarios)
          //.WithMany(d => d.OrdenCompras)
          //.HasForeignKey(d => d.UsuarioId)
          //.OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Venta>()
                .HasOne(d => d.DetalleVenta)
                .WithMany(d => d.Ventas)
                .HasForeignKey(d => d.DetalleVentaId)
                .OnDelete(DeleteBehavior.Restrict);

           builder.Entity<Compra>()
               .HasOne(d => d.DetalleCompra)
               .WithMany(d => d.Compras)
               .HasForeignKey(d => d.DetalleCompraId)
               .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<OrdenCompra>()
            //.HasOne(d => d.Usuarios)
            //.WithMany(d => d.OrdenCompras)
            //.HasForeignKey(d => d.UsuarioId)
            //.OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Proveedores>().HasIndex(r => r.Rif).IsUnique();
            builder.Entity<Usuario>().HasIndex(r => r.Username).IsUnique();


            builder.Entity<Rol>().HasData(
                new { Id = 1, Tipo = "Admin"},
                new { Id = 2, Tipo = "Cliente" },
                new { Id = 3, Tipo = "Empleado" });

            builder.Entity<UsuarioSession>().HasData(
                new { Id = 1, UsuarioId = 1, RolId = 1});

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordEncrypt = BCrypt.Net.BCrypt.HashPassword("jesus", salt);

            builder.Entity<Usuario>().HasData(
                new { Id = 1, Nombre = "Jesus", Apellido = "Granados", Direccion = "La concordia", Username = "jesus", Password = passwordEncrypt, UsuarioSessionId = 1 });

            
            builder.Entity<RolUsuario>().HasData(
                new { UsuarioId = 1, RolId = 1 });


           builder.Entity<Categoria>().HasData(
                new { Id = 1, TipoCategoria = "harinas" });





        }







    }
}
