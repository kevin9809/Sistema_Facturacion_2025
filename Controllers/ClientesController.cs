using Proyecto_MVC.Models;
using Proyecto_MVC.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Proyecto_MVC.Controllers
{
    public class ClientesController : Controller
    {
        ClientesRepository clientes = new ClientesRepository();
        // GET: Clientes
        public ActionResult Index(string busqueda)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Login", "Account");

            var listaClientes = clientes.CargarClientes();

            // Buscar clientes si hay texto
            if(!string.IsNullOrEmpty(busqueda))
            {
                var resultados = listaClientes.Where(c =>
                c.Nombre.Contains(busqueda) ||
                c.Email.Contains(busqueda) ||
                c.Telefono.Contains(busqueda));
                return View(resultados);
            }

            return View(listaClientes);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Clientes cliente)
        {
            if (ModelState.IsValid)
            {
                clientes.InsertarCliente(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        public ActionResult Editar(int id)
        {
            var cliente = clientes.CargarClientes()
                .FirstOrDefault(c => c.ID_Cliente == id);

            if(cliente == null) return HttpNotFound();
            return View(cliente);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Clientes cliente)
        {
            if (ModelState.IsValid)
            {
                clientes.EditarCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public ActionResult Eliminar(int id)
        {
            var cliente = clientes.CargarClientes()
                .FirstOrDefault(c => c.ID_Cliente == id);

            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        [HttpPost, ActionName("Eliminar")] // Mantiene la URL /Eliminar/
        [ValidateAntiForgeryToken]
        public ActionResult EliminarConfirmado(int id)
        {            
            clientes.EliminarCliente(id);
            return RedirectToAction("Index");
        }
    }
}