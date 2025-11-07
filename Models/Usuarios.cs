using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Models
{
    public class Usuarios
    {
        [Key]
        public int ID_Usuario { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingres la contraseña")]
        [DataType(DataType.Password)]
        public string  Contraseña { get; set; }
        public string Email { get; set; }
        public string  Rol { get; set; }
    }
}