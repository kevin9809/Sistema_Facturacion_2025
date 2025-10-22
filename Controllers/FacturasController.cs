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
        private const decimal IVA_RATE = 0.13m; // Se usa en CalcularFactura


        public ActionResult Index(string search)
        {
            IEnumerable<Facturas> facturas = new List<Facturas>();
            try
            {
                // Llama al repositorio para obtener todas las facturas (o las filtradas)
                facturas = _facturaRepo.ObtenerTodasLasFacturas(search);

                // Pasa el término de búsqueda a la vista para que el campo de texto mantenga el valor
                ViewBag.SearchTerm = search;
            }
            catch (Exception ex)
            {
                // Si hay un error de conexión o en la query, se muestra en la vista
                ViewBag.Error = "Error al cargar el listado de facturas: " + ex.Message;
            }
            // Retorna la vista, pasando la lista de facturas (puede ser vacía)
            return View(facturas);
        }

        // GET: Facturas/NuevaFactura
        public ActionResult NuevaFactura()
        {
            // Limpiar la sesión e inicializar
            Session["FacturaItems"] = new List<FacturaItem>();
            Session["ClienteObj"] = null; // Limpiamos el cliente de la sesión
            ViewBag.Cliente = null;
            return View(CalcularFactura()); // Retornar Facturas con Items vacíos
        }

        // POST: Facturas/BuscarCliente
        [HttpPost]
        public ActionResult BuscarCliente(string nombreCliente)
        {
            var cliente = _clienteRepo.ObtenerClientePorNombre(nombreCliente);

            if (cliente != null)
            {
                // CORRECCIÓN CRÍTICA: Guardar el objeto cliente completo en Session
                Session["ClienteObj"] = cliente;
                ViewBag.Cliente = cliente;
            }
            else
            {
                ViewBag.Error = "Cliente no encontrado.";
            }

            // Recuperar el cliente de la sesión para la vista (si existe)
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
                    ProductoID = productoEncontrado.ProductoID,
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


            if (Session["ClienteObj"] != null)
            {
                ViewBag.Cliente = (Proyecto_MVC.Models.Clientes)Session["ClienteObj"];
            }

            // Recalcular y retornar la vista
            return View("NuevaFactura", CalcularFactura());
        }

        // POST: Facturas/Guardar
        [HttpPost]
        public ActionResult Guardar(FormCollection form)
        {
            int nuevoId = 0; // Inicializamos el ID en 0

            try
            {
                var factura = CalcularFactura(); // Obtiene la factura completa con totales

                // Obtener ClienteID desde el objeto Cliente en la sesión
                var cliente = (Proyecto_MVC.Models.Clientes)Session["ClienteObj"];

                if (cliente == null)
                {
                    ViewBag.Error = "Debe buscar y seleccionar un cliente antes de guardar la factura.";
                    return View("NuevaFactura", CalcularFactura());
                }

                factura.ClienteID = cliente.ID_Cliente; // Asigna el ID del cliente

                // El repositorio devuelve el ID de la factura (int).
                nuevoId = _facturaRepo.GuardarFactura(factura);

                if (nuevoId > 0)
                {
                    // Usar el ID de la factura en el mensaje de éxito
                    TempData["MensajeExito"] = $"¡Factura número {nuevoId} guardada correctamente! Puedes buscarla en el listado.";

                    Session["FacturaItems"] = null; // Limpiar sesión
                    Session["ClienteObj"] = null;
                    return RedirectToAction("Index"); // Redirigir a la lista de facturas
                }

                // Si el repositorio devuelve 0, hubo un problema no controlado por la excepción.
                ViewBag.Error = "Error al guardar la factura en la base de datos (ID no generado).";
            }
            catch (Exception ex)
            {
                // Si el repositorio lanza una excepción, la atrapamos aquí.
                ViewBag.Error = "Error al guardar la factura: " + ex.Message;
            }

            // Si hubo un error, recargamos la vista de NuevaFactura con los datos y el error
            if (Session["ClienteObj"] != null)
            {
                ViewBag.Cliente = (Proyecto_MVC.Models.Clientes)Session["ClienteObj"];
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