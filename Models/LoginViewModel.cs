using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingresa tu nombre, por favor.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresa tu contraseña por favor.")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
    }
}