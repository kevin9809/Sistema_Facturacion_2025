using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages.Facturas
{
    // Clase que representa un artículo en el GridView de la factura.
    [Serializable]
    public class FacturaItem
    {
        public int ProductoID { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }

    public partial class Facturas : System.Web.UI.Page
    {
        ConexionDB conexion = new ConexionDB();
        private const decimal IVA_RATE = 0.13m; // Tasa de IVA

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializa la lista de artículos de la factura en la sesión.
                Session["FacturaItems"] = new List<FacturaItem>();
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
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    // Consulta para buscar cliente por nombre
                    string query = "SELECT Direccion, Telefono, Email FROM Clientes WHERE Nombre = @Nombre";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", clienteInput);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            txtDireccion.Text = reader["Direccion"].ToString();
                            txtTelefono.Text = reader["Telefono"].ToString();
                            txtDocumento.Text = reader["Email"].ToString(); // Asumimos que el "Documento" es el email en este caso, si hay una columna de documento en la tabla, se debe cambiar.
                        }
                        else
                        {
                            // Si no se encuentra el cliente, limpia los campos
                            txtDireccion.Text = "";
                            txtTelefono.Text = "";
                            txtDocumento.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                lblArticuloMensaje.Text = "Error al buscar cliente: " + ex.Message;
            }
        }

        protected void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                string articuloInput = txtArticulo.Text.Trim();
                int cantidad;

                if (string.IsNullOrEmpty(articuloInput) || !int.TryParse(txtCantidad.Text.Trim(), out cantidad) || cantidad <= 0)
                {
                    lblArticuloMensaje.Text = "Por favor, ingrese un artículo y una cantidad válida.";
                    return;
                }

                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    // Consulta para buscar artículo por nombre o ID en la tabla 'Productos'
                    string query = "SELECT ProductoID, NombreProducto, Precio FROM Productos WHERE ProductoID = @Input OR NombreProducto = @Input";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Input", articuloInput);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int idArticulo = reader.GetInt32(0);
                            string nombreArticulo = reader.GetString(1);
                            decimal precio = reader.GetDecimal(2);
                            decimal totalArticulo = precio * cantidad;

                            var item = new FacturaItem
                            {
                                ProductoID = idArticulo,
                                NombreProducto = nombreArticulo,
                                Precio = precio,
                                Cantidad = cantidad,
                                Total = totalArticulo
                            };

                            // Agregar el nuevo item a la lista de la sesión
                            var facturaItems = (List<FacturaItem>)Session["FacturaItems"];
                            facturaItems.Add(item);

                            // Actualizar el GridView y los totales
                            gvFacturaDetalles.DataSource = facturaItems;
                            gvFacturaDetalles.DataBind();
                            CalcularTotales();
                            lblArticuloMensaje.Text = ""; // Limpiar mensaje de error
                        }
                        else
                        {
                            lblArticuloMensaje.Text = "Artículo no encontrado.";
                        }
                    }
                }
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
                // Validación básica de campos
                if (string.IsNullOrEmpty(txtCliente.Text) || ((List<FacturaItem>)Session["FacturaItems"]).Count == 0)
                {
                    lblArticuloMensaje.Text = "Por favor, seleccione un cliente y agregue al menos un artículo.";
                    return;
                }

                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        // 1. Insertar en la tabla Facturas
                        string insertFacturaQuery = "INSERT INTO Facturas (ClienteID, Fecha, Total) VALUES (@ClienteID, @Fecha, @Total); SELECT SCOPE_IDENTITY();";

                        int clienteID = ObtenerIdCliente(txtCliente.Text.Trim());
                        if (clienteID == -1)
                        {
                            throw new Exception("Cliente no encontrado.");
                        }

                        // Calcular el total de la factura
                        decimal totalFactura = 0;
                        var facturaItems = (List<FacturaItem>)Session["FacturaItems"];
                        foreach (var item in facturaItems)
                        {
                            totalFactura += item.Total;
                        }

                        SqlCommand cmdFactura = new SqlCommand(insertFacturaQuery, con, transaction);
                        cmdFactura.Parameters.AddWithValue("@ClienteID", clienteID);
                        cmdFactura.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        cmdFactura.Parameters.AddWithValue("@Total", totalFactura);

                        int facturaID = Convert.ToInt32(cmdFactura.ExecuteScalar());

                        // 2. Insertar en la tabla DetalleFactura
                        string insertDetalleQuery = "INSERT INTO DetalleFactura (FacturaID, ProductoID, Cantidad, PrecioUnitario, Subtotal) VALUES (@FacturaID, @ProductoID, @Cantidad, @PrecioUnitario, @Subtotal)";

                        foreach (var item in facturaItems)
                        {
                            SqlCommand cmdDetalle = new SqlCommand(insertDetalleQuery, con, transaction);
                            cmdDetalle.Parameters.AddWithValue("@FacturaID", facturaID);
                            cmdDetalle.Parameters.AddWithValue("@ProductoID", item.ProductoID);
                            cmdDetalle.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                            cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", item.Precio);
                            cmdDetalle.Parameters.AddWithValue("@Subtotal", item.Total);

                            cmdDetalle.ExecuteNonQuery();
                        }

                        // 3. Confirmar la transacción y mostrar mensaje de éxito
                        transaction.Commit();
                        lblArticuloMensaje.Text = "Factura número " + facturaID + " creada exitosamente.";
                        lblArticuloMensaje.ForeColor = System.Drawing.Color.Green;

                        LimpiarFormularioFactura();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        lblArticuloMensaje.Text = "Error al guardar la factura: " + ex.Message;
                        lblArticuloMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblArticuloMensaje.Text = "Error de conexión o de datos: " + ex.Message;
                lblArticuloMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private int ObtenerIdCliente(string nombreCliente)
        {
            int clienteID = -1;
            using (SqlConnection con = conexion.Conectar())
            {
                con.Open();
                string query = "SELECT ID_Cliente FROM Clientes WHERE Nombre = @Nombre";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreCliente);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        clienteID = Convert.ToInt32(result);
                    }
                }
            }
            return clienteID;
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

                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string queryDetalles = "SELECT T2.ProductoID, T1.NombreProducto AS Descripcion, T2.PrecioUnitario, T2.Cantidad, T2.Subtotal AS Total " +
                                           "FROM DetalleFactura T2 " +
                                           "INNER JOIN Productos T1 ON T2.ProductoID = T1.ProductoID " +
                                           "WHERE T2.FacturaID = @FacturaID";

                    string queryCliente = "SELECT T1.Nombre, T1.Direccion, T1.Telefono, T1.Email, T2.Fecha FROM Clientes T1 " +
                                          "INNER JOIN Facturas T2 ON T1.ID_Cliente = T2.ClienteID " +
                                          "WHERE T2.FacturaID = @FacturaID";

                    using (SqlCommand cmdDetalles = new SqlCommand(queryDetalles, con))
                    {
                        cmdDetalles.Parameters.AddWithValue("@FacturaID", numeroFactura);
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmdDetalles);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            // Se encontró la factura, ahora busca los datos del cliente
                            using (SqlCommand cmdCliente = new SqlCommand(queryCliente, con))
                            {
                                cmdCliente.Parameters.AddWithValue("@FacturaID", numeroFactura);
                                SqlDataReader reader = cmdCliente.ExecuteReader();
                                if (reader.Read())
                                {
                                    lblClienteNombre.Text = reader["Nombre"].ToString();
                                    lblDireccionFactura.Text = reader["Direccion"].ToString();
                                    lblTelefonoFactura.Text = reader["Telefono"].ToString();
                                    lblCorreo.Text = reader["Email"].ToString();
                                    lblFecha.Text = Convert.ToDateTime(reader["Fecha"]).ToString("dd/MM/yyyy");
                                }
                                reader.Close();
                            }

                            gvFacturaEncontradaDetalles.DataSource = dt;
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
                    }
                }
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