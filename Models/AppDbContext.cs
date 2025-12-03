using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=InventarioDB") { }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<FacturaItem> FacturaItems { get; set; }

    }
}