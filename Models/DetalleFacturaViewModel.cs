using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Models
{
    public class DetalleFacturaViewModel
    {
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; } 
    }
}