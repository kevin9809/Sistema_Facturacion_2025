using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_MVC.Models
{
    public class FacturaDetalleViewModel
    {
        public FacturaDetalleViewModel()
        {
            Detalles = new List<DetalleFacturaViewModel>();
            Clientes = new List<SelectListItem>();
            Productos = new List<Productos>();
        }
        public DateTime Fecha { get; set; }
        public int ClienteID { get; set; }
        public List<SelectListItem> Clientes { get; set; }

        public int ProductoID { get; set; }

        public List<Productos> Productos { get; set; }
        public List<DetalleFacturaViewModel> Detalles { get; set; }

        public decimal Total { get; set; }
    }
}