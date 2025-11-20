using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Proyecto_MVC.Models;
using Proyecto_MVC.Repositorio;

namespace Proyecto_MVC.Controllers
{
    public class FacturasController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        public ActionResult Index(string buscar)
        {
            var lista = (from f in db.Facturas
                         join c in db.Clientes on f.ClienteID equals c.ID_Cliente
                         select new ListaFacturasViewModel
                         {
                             FacturaID = f.FacturaID,
                             Fecha = f.Fecha,
                             NombreCliente = c.Nombre,
                             Total = f.Total
                         });

            if (!string.IsNullOrEmpty(buscar))
            {
                lista = lista.Where(x =>
                    x.NombreCliente.Contains(buscar) ||
                    x.FacturaID.ToString().Contains(buscar));
            }

            return View(lista.ToList());
        }

        public ActionResult Detalles(int id)
        {
            var lista = (from d in db.DetalleFactura
                         join p in db.Productos on d.ProductoID equals p.ProductoID
                         where d.FacturaID == id
                         select new DetalleFacturaViewModel
                         {
                             Cantidad = d.Cantidad,
                             NombreProducto = p.NombreProducto,
                             DescripcionProducto = p.Descripcion,
                             PrecioUnitario = d.PrecioUnitario,
                             Subtotal = d.Subtotal
                         }).ToList();

            // Obtener la factura
            var factura = db.Facturas.FirstOrDefault(f => f.FacturaID == id);

            if (factura == null)
            {
                return HttpNotFound("No se encontró la factura especificada.");
            }

            // Obtener el cliente relacionado
            var cliente = db.Clientes.FirstOrDefault(c => c.ID_Cliente == factura.ClienteID);

            ViewBag.DatosCliente = cliente;
            ViewBag.Id = id;
            ViewBag.Fecha = factura.Fecha.ToShortDateString();
            ViewBag.Total = factura.Total;

            if (!lista.Any())
            {
                return HttpNotFound("No se encontraron detalles para esta factura.");
            }

            return View(lista);
        }

        public ActionResult Crear()
        {
            var modelo = new FacturaDetalleViewModel
            {
                Fecha = DateTime.Now,
                Clientes = db.Clientes
                             .Select(c => new SelectListItem
                             {
                                 Value = c.ID_Cliente.ToString(),
                                 Text = c.Nombre
                             })
                             .ToList(),
                Productos = db.Productos.ToList(),
                Detalles = new List<DetalleFacturaViewModel>()
            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(FacturaDetalleViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var factura = new Facturas
                {
                    Fecha = modelo.Fecha,
                    ClienteID = modelo.ClienteID,
                    Total = modelo.Total
                };

                db.Facturas.Add(factura);
                db.SaveChanges();

                // Guardar detalles
                foreach (var item in modelo.Detalles)
                {
                    var detalle = new DetalleFactura
                    {
                        FacturaID = factura.FacturaID,
                        ProductoID = item.ProductoID,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario
                    };
                    db.DetalleFactura.Add(detalle);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Si algo falla, recarga la lista de clientes
            modelo.Clientes = db.Clientes
                                .Select(c => new SelectListItem
                                {
                                    Value = c.ID_Cliente.ToString(),
                                    Text = c.Nombre
                                }).ToList();

            return View(modelo);
        }

        // GET: Facturas/Eliminar
        public ActionResult Eliminar(int id)
        {
            var factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }

            // Puedes incluir datos del cliente si los tienes relacionados
            var cliente = db.Clientes.FirstOrDefault(c => c.ID_Cliente == factura.ClienteID);

            ViewBag.Cliente = cliente;
            return View(factura);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarConfirmado(int id)
        {
            try
            {
                var factura = db.Facturas.Find(id);
                if (factura == null)
                    return HttpNotFound();

                var detalles = db.DetalleFactura.Where(d => d.FacturaID == id).ToList();
                db.DetalleFactura.RemoveRange(detalles);
                db.Facturas.Remove(factura);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al eliminar la factura: " + ex.Message;
                return View("Error");
            }
        }

    }
}