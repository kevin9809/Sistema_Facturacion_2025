using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Models
{
    public class ListaFacturasViewModel
    {
        public int FacturaID { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreCliente { get; set; }
        public decimal Total { get; set; }
    }
}