using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Proyecto_progra1_v1.Modelos.Entidades;      
using Proyecto_progra1_v1.Modelos.Repositorios;


namespace Proyecto_progra1_v1.Pages.Facturas    
{

    public partial class Facturas : System.Web.UI.Page
    {
        ConexionDB conexion = new ConexionDB();
        private const decimal IVA_RATE = 0.13m; // Tasa de IVA
        private ClienteRepository clienteRepo = new ClienteRepository();
        private FacturaRepository facturaRepo = new FacturaRepository();
        private ProductoRepository productoRepo = new ProductoRepository();

        private void CargarFacturaGridView()
        {
            // Asume que tu GridView se llama GridViewFactura. 
            // Si tiene otro nombre (ej: gvArticulos), cámbialo.
            if (Session["FacturaItems"] is List<FacturaItem> facturaItems)
            {
                gvFacturaDetalles.DataSource = facturaItems;
                gvFacturaDetalles.DataBind();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializa la lista de artículos de la factura en la sesión.
                if (Session["FacturaItems"] == null)
                {
                    Session["FacturaItems"] = new List<FacturaItem>();
                }
                pnlOpcionesFactura.Visible = true;
                pnlNuevaFactura.Visible = false;
                pnlVerFactura.Visible = false;
            }
        }

        protected void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            pnlOpcionesFactura.Visible = false;
            pnlVerFactura.Visible = false;
            pnlNuevaFactura.Visible = true;
            LimpiarFormularioFactura();
        }

        protected void btnVerFactura_Click(object sender, EventArgs e)
        {
            pnlOpcionesFactura.Visible = false;
            pnlNuevaFactura.Visible = false;
            pnlVerFactura.Visible = true;
            LimpiarFormularioBusqueda();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            pnlNuevaFactura.Visible = false;
            pnlVerFactura.Visible = false;
            pnlOpcionesFactura.Visible = true;
        }

        private void CargarDatosCliente(string clienteInput)
        {
            try
            {
                // Llama al Repositorio para obtener la entidad Cliente
                Cliente cliente = clienteRepo.ObtenerClientePorNombre(clienteInput);

                if (cliente != null)
                {
                    // Usa las propiedades de la entidad Cliente para rellenar los TextBox
                    txtDireccion.Text = cliente.Direccion;
                    txtTelefono.Text = cliente.Telefono;
                    txtDocumento.Text = cliente.Email;
                }
                else
                {
                    // Si no se encuentra, limpia los campos
                    txtDireccion.Text = "";
                    txtTelefono.Text = "";
                    txtDocumento.Text = "";
                }
            }
            catch (Exception ex)
            {
                // El repositorio ya debería manejar errores internos, esto es para otros errores.
                lblArticuloMensaje.Text = "Error al buscar cliente: " + ex.Message;
            }
        }

        protected void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                string busquedaProducto = txtArticulo.Text.Trim();
                int cantidad = int.Parse(txtCantidad.Text.Trim());

                // 1. LLAMADA AL REPOSITORIO (M)
                Producto productoEncontrado = productoRepo.ObtenerProductoPorIdONombre(busquedaProducto);

                if (productoEncontrado == null)
                {
                    lblArticuloMensaje.Text = "Error: Producto no encontrado.";
                    return;
                }

                if (cantidad <= 0)
                {
                    lblArticuloMensaje.Text = "Error: La cantidad debe ser mayor a cero.";
                    return;
                }

                // 2. Lógica de Negocio (Verificar Stock)
                if (cantidad > productoEncontrado.Stock)
                {
                    lblArticuloMensaje.Text = $"Error: Stock insuficiente. Disponible: {productoEncontrado.Stock}";
                    return;
                }

                // 3. Crear el item y agregarlo a la sesión (Vista/Controlador)
                FacturaItem nuevoItem = new FacturaItem
                {
                    ProductoID = productoEncontrado.ProductoID,
                    NombreProducto = productoEncontrado.NombreProducto,
                    Precio = productoEncontrado.Precio,
                    Cantidad = cantidad,
                    Total = productoEncontrado.Precio * cantidad
                };

                // ... El resto de tu lógica para agregar a la Session y actualizar el GridView ...
                List<FacturaItem> facturaItems = (List<FacturaItem>)Session["FacturaItems"];
                facturaItems.Add(nuevoItem);
                CargarFacturaGridView(); // El método que hace DataBind
                CalcularTotales();
            }
            catch (Exception ex)
            {
                lblArticuloMensaje.Text = "Error al agregar artículo: " + ex.Message;
            }
        }

        protected void gvFacturaDetalles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var facturaItems = (List<FacturaItem>)Session["FacturaItems"];
            int index = e.RowIndex;
            facturaItems.RemoveAt(index);

            // Actualizar el GridView y los totales
            gvFacturaDetalles.DataSource = facturaItems;
            gvFacturaDetalles.DataBind();
            CalcularTotales();
        }

        private void CalcularTotales()
        {
            var facturaItems = (List<FacturaItem>)Session["FacturaItems"];
            decimal subtotal = 0;
            foreach (var item in facturaItems)
            {
                subtotal += item.Total;
            }

            decimal iva = subtotal * IVA_RATE;
            decimal total = subtotal + iva;

            txtTotalParcial.Text = subtotal.ToString("N2");
            txtIVA.Text = iva.ToString("N2");
            txtTotal.Text = total.ToString("N2");
        }

        protected void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCliente.Text) || ((List<FacturaItem>)Session["FacturaItems"]).Count == 0)
                {
                    lblArticuloMensaje.Text = "Por favor, seleccione un cliente y agregue al menos un artículo.";
                    return;
                }

                int clienteID = ObtenerIdCliente(txtCliente.Text.Trim());
                if (clienteID == -1)
                {
                    throw new Exception("Cliente no encontrado o ID inválido.");
                }

                // --- CÓDIGO NUEVO Y MODIFICADO ---

                // 1. Obtener los ítems y calcular el Subtotal
                decimal subtotalFactura = 0;
                var facturaItems = (List<FacturaItem>)Session["FacturaItems"];

                foreach (var item in facturaItems)
                {
                    subtotalFactura += item.Total; // item.Total ya es Cantidad * Precio
                }

                // 2. Calcular el IVA y el Total
                // Nota: Asumo que tienes una constante o campo llamado IVA_RATE = 0.13m (13%)
                const decimal IVA_RATE = 0.13m; // Asegúrate de que esta constante esté definida en tu clase

                decimal ivaFactura = subtotalFactura * IVA_RATE;
                decimal totalFactura = subtotalFactura + ivaFactura;

                // 3. Llamar al repositorio con los 3 valores: Subtotal, IVA y Total
                // DEBES MODIFICAR LA FIRMA DE GuardarFactura en FacturaRepository.cs para recibir estos 3 valores
                int facturaID = facturaRepo.GuardarFactura(clienteID, subtotalFactura, ivaFactura, totalFactura, facturaItems);

                // --- FIN DEL CÓDIGO MODIFICADO ---

                lblArticuloMensaje.Text = "Factura número " + facturaID + " creada exitosamente.";
                lblArticuloMensaje.ForeColor = System.Drawing.Color.Green;

                LimpiarFormularioFactura();
            }
            catch (Exception ex)
            {
                lblArticuloMensaje.Text = "Error al guardar la factura: " + ex.Message;
                lblArticuloMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private int ObtenerIdCliente(string nombreCliente)
        {
            // Llama al Repositorio para obtener solo el ID.
            return clienteRepo.ObtenerIdCliente(nombreCliente);
        }

        protected void btnBuscarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                int numeroFactura;
                if (!int.TryParse(txtNumeroFactura.Text.Trim(), out numeroFactura))
                {
                    lblBusquedaMensaje.Text = "Por favor, ingrese un número de factura válido.";
                    pnlFacturaEncontrada.Visible = false;
                    return;
                }

                // --- INICIO DE LA REFACTORIZACIÓN (USO DEL REPOSITORIO) ---

                // 1. Llamar al repositorio para obtener la factura COMPLETA (Cabecera y Detalles)
                // Necesitas un método en ClienteRepository que te devuelva el Cliente completo o el nombre
                Factura factura = facturaRepo.ObtenerFacturaPorId(numeroFactura);

                if (factura != null)
                {
                    // 2. Obtener los datos del cliente por su ID (usando el repositorio)
                    // Asumo que tu ClienteRepository tiene un método para obtener el cliente completo por ID
                    Cliente cliente = clienteRepo.ObtenerClientePorId(factura.ClienteID);

                    // 3. Rellenar los Labels de Cliente y Cabecera
                    if (cliente != null)
                    {
                        lblClienteNombre.Text = cliente.Nombre; // Asumo que la propiedad es 'Nombre'
                        lblDireccionFactura.Text = cliente.Direccion;
                        lblTelefonoFactura.Text = cliente.Telefono;
                        lblCorreo.Text = cliente.Email; // O la propiedad que uses para Email/Documento
                    }

                    lblFecha.Text = factura.Fecha.ToString("dd/MM/yyyy");

                    // 4. MOSTRAR LOS NUEVOS TOTALES (Subtotal e IVA)
                    lblTotalParcialFactura.Text = factura.SubtotalFactura.ToString("N2"); // Necesitas crear este Label en tu ASPX
                    lblIVAFactura.Text = factura.IVAFactura.ToString("N2");             // Necesitas crear este Label en tu ASPX
                    lblTotalFactura.Text = factura.Total.ToString("N2");             // Necesitas crear este Label en tu ASPX

                    // 5. Cargar los detalles en el GridView de búsqueda
                    gvFacturaEncontradaDetalles.DataSource = factura.Items; // Usar la lista de ítems cargada por el repositorio
                    gvFacturaEncontradaDetalles.DataBind();

                    lblBusquedaMensaje.Text = "Factura encontrada.";
                    lblBusquedaMensaje.ForeColor = System.Drawing.Color.Green;
                    pnlFacturaEncontrada.Visible = true;
                    lblFacturaEncontradaNum.Text = numeroFactura.ToString();
                }
                else
                {
                    lblBusquedaMensaje.Text = "No se encontró la factura con el número proporcionado.";
                    lblBusquedaMensaje.ForeColor = System.Drawing.Color.Red;
                    pnlFacturaEncontrada.Visible = false;
                }

                // --- FIN DE LA REFACTORIZACIÓN ---
            }
            catch (Exception ex)
            {
                lblBusquedaMensaje.Text = "Error al buscar la factura: " + ex.Message;
                lblBusquedaMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LimpiarFormularioFactura()
        {
            txtCliente.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtArticulo.Text = string.Empty;
            txtCantidad.Text = "1";
            txtTotalParcial.Text = string.Empty;
            txtIVA.Text = string.Empty;
            txtTotal.Text = string.Empty;

            // Limpiar el GridView
            var facturaItems = (List<FacturaItem>)Session["FacturaItems"];
            facturaItems.Clear();
            gvFacturaDetalles.DataSource = null;
            gvFacturaDetalles.DataBind();
        }

        private void LimpiarFormularioBusqueda()
        {
            txtNumeroFactura.Text = string.Empty;
            lblBusquedaMensaje.Text = string.Empty;
            pnlFacturaEncontrada.Visible = false;
        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            string nombreCliente = txtCliente.Text.Trim();
            CargarDatosCliente(nombreCliente);
        }
    }
}