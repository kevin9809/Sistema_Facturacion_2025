using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_MVC.Models
{
    public class Facturas
    {
        // Propiedades de la Cabecera de la Factura
        public int FacturaID { get; set; }

        // Clave Foránea al Cliente
        [Display(Name = "Cliente")]
        public int ClienteID { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Propiedades de totales (Se calculan a partir de Items)
        public decimal SubtotalFactura { get; set; }
        public decimal IVAFactura { get; set; }
        public decimal Total { get; set; }

        // Propiedad de navegación (La lista de artículos)
        public List<FacturaItem> Items { get; set; } = new List<FacturaItem>();

        // Propiedad para la vista (Opcional: nombre del cliente)
        public string NombreCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string EmailCliente { get; set; }
    }
}