using Proyecto_MVC.Models;
using Proyecto_MVC.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_MVC.Controllers
{
    public class AccountController : Controller
    {
        UsuariosRepository usuarios = new UsuariosRepository();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Busca al usuario
                var usuario = usuarios.CargarUsuarios()
                    .FirstOrDefault(u => 
                    u.Nombre == model.Nombre && 
                    u.Contraseña == model.Contraseña);

                if (usuario != null)
                {
                    // Guarda en sesión
                    Session["Usuario"] = usuario.Nombre;
                    Session["Rol"] = usuario.Rol;

                    // Redirige al Home o al área según el rol
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = "Usuario o contraseña incorrectos";
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
