using Proyecto_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Repositorio
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=InventarioDB") { }

        // Métodos para obtener las tablas
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<DetalleFactura> DetalleFactura { get; set; }
        
    }
}