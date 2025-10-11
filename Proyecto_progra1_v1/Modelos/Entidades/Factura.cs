using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_progra1_v1.Modelos.Entidades
{
    public class Factura
    {
        public int FacturaID { get; set; }
        public int ClienteID { get; set; }
        public DateTime Fecha { get; set; }

        // Totales de la Factura Completa
        public decimal SubtotalFactura { get; set; }
        public decimal IVAFactura { get; set; }
        public decimal Total { get; set; }

        // Colección de los detalles de la factura
        public List<FacturaItem> Items { get; set; } = new List<FacturaItem>();
    }
}
