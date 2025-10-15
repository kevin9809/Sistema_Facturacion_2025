﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Models
{
    public class Productos
    {
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }
    }
}