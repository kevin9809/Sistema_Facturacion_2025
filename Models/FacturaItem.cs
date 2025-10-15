using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Models
{
    public class FacturaItem
    {
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        // El total de la línea (Precio * Cantidad)
        public decimal Total { get; set; }

        // Alias para compatibilidad con GridView u otra lógica
        public int ID_Artículo
        {
            get { return ProductoID; }
        }
    }
}