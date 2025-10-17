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
        private readonly FacturaRepository _facturaRepo = new FacturaRepository();
        private readonly ClientesRepository _clienteRepo = new ClientesRepository();
        private readonly InventarioRepository _productoRepo = new InventarioRepository();
        private const decimal IVA_RATE = 0.13m;

        // GET: Facturas/Index
        public ActionResult Index()
        {
            // Mostrar lista de facturas
            return View(); // Views/Facturas/Index.cshtml
        }

        // GET: Facturas/NuevaFactura
        public ActionResult NuevaFactura()
        {
            // Inicializar la lista de artículos en la sesión
            Session["FacturaItems"] = new List<FacturaItem>();
            ViewBag.Cliente = null;
            return View(new Facturas()); // Views/Facturas/NuevaFactura.cshtml
        }

        // POST: Facturas/BuscarCliente
        [HttpPost]
        public ActionResult BuscarCliente(string nombreCliente)
        {
            var cliente = _clienteRepo.ObtenerClientePorNombre(nombreCliente);
            if (cliente != null)
            {
                // Guardar ClienteID en Session para la factura
                Session["ClienteID"] = cliente.ID_Cliente;
                // Retornar a la vista con el cliente cargado
                ViewBag.Cliente = cliente;
            }
            else
            {
                ViewBag.Error = "Cliente no encontrado.";
            }
            if (Session["ClienteObj"] != null)
{
    ViewBag.Cliente = (Proyecto_MVC.Models.Clientes)Session["ClienteObj"];
}
            // Recargar la vista con los datos actuales
            return View("NuevaFactura", CalcularFactura());
        }

        // POST: Facturas/AgregarArticulo
        [HttpPost]
        public ActionResult AgregarArticulo(string articulo, int cantidad)
        {
            var items = (List<FacturaItem>)Session["FacturaItems"];

            // Buscar el producto por ID o Nombre (usando tu ProductoRepository)
            var productoEncontrado = _productoRepo.ObtenerProductoPorNomOId(articulo);

            if (productoEncontrado != null && cantidad > 0)
            {
                var nuevoItem = new FacturaItem
                {
                    ProductoID = productoEncontrado.ProductoID, // Asumo que el producto tiene una propiedad ID
                    NombreProducto = productoEncontrado.NombreProducto,
                    Precio = productoEncontrado.Precio,
                    Cantidad = cantidad,
                    Total = productoEncontrado.Precio * cantidad
                };
                items.Add(nuevoItem);
                Session["FacturaItems"] = items;
            }
            else
            {
                ViewBag.Error = "Artículo no encontrado o cantidad inválida.";
            }

            // Recalcular y retornar la vista
            return View("NuevaFactura", CalcularFactura());
        }

        // POST: Facturas/Guardar
        [HttpPost]
        public ActionResult Guardar(FormCollection form)
        {
            try
            {
                var factura = CalcularFactura(); // Obtiene la factura completa con totales
                factura.ClienteID = (int)Session["ClienteID"]; // Asigna el cliente

                if (_facturaRepo.GuardarFactura(factura))
                {
                    Session["FacturaItems"] = null; // Limpiar sesión
                    Session["ClienteID"] = null;
                    return RedirectToAction("Index"); // Redirigir a la lista de facturas
                }
                ViewBag.Error = "Error al guardar la factura en la base de datos.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al guardar la factura: " + ex.Message;
            }
            return View("NuevaFactura", CalcularFactura());
        }

        // GET: Facturas/Ver/13
        public ActionResult Ver(int id)
        {
            var factura = _facturaRepo.ObtenerFacturaPorId(id);

            if (factura == null)
            {
                ViewBag.Error = "Factura no encontrada.";
                return View("Index");
            }
            return View(factura); // Views/Facturas/Ver.cshtml
        }

        // Método de Lógica Común para calcular totales
        private Facturas CalcularFactura()
        {
            var items = (List<FacturaItem>)Session["FacturaItems"] ?? new List<FacturaItem>();

            var subtotal = items.Sum(i => i.Total);
            var iva = subtotal * IVA_RATE;
            var total = subtotal + iva;

            return new Facturas
            {
                SubtotalFactura = subtotal,
                IVAFactura = iva,
                Total = total,
                Items = items
            };
        }
    }
}