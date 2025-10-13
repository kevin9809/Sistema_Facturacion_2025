using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 

namespace Proyecto_progra1_v1.Modelos.Entidades
{
    // Clase de Entidad para una LÍNEA DE DETALLE de la factura
    [Serializable]
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
