using Proyecto_MVC.Models;
using Proyecto_MVC.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_MVC.Controllers
{
    public class InventarioController : Controller
    {
        InventarioRepository productos = new InventarioRepository();
        // GET: Inventario
        public ActionResult Index(string busqueda)
        {
            var listaProductos = productos.CargarProductos();

            if (!string.IsNullOrEmpty(busqueda))
            {
                var resultados = listaProductos.Where(p =>
                p.NombreProducto.Contains(busqueda) || 
                p.Descripcion.Contains(busqueda));
                return View(resultados);
            }
            return View(listaProductos);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Productos producto) 
        {
            if (ModelState.IsValid)
            {
                productos.InsertarProducto(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        public ActionResult Editar(int id)
        {
            var producto = productos.CargarProductos()
                .FirstOrDefault(p => p.ProductoID == id);    
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Productos producto)
        {
            if (ModelState.IsValid) 
            {
                productos.ActualizarProducto(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        public ActionResult Eliminar(int id)
        {
            var producto = productos.CargarProductos()
                .FirstOrDefault(p => p.ProductoID == id);
            return View(producto);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarConfirmacion(int id)
        {
            productos.EliminarProducto(id);
            return RedirectToAction("Index");
        }
    }
}