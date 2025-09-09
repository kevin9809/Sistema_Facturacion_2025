using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Proyecto_progra1_v1.Pages.Inventory
{
    public partial class EditarInventario : System.Web.UI.Page
    {
        // Instancia de tu clase de conexión a la BD
        ConexionDB conexion = new ConexionDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int productoID;
                    if (int.TryParse(Request.QueryString["id"], out productoID))
                    {
                        CargarProducto(productoID);
                    }
                    else
                    {
                        // Si el ID no es válido, muestra un error.
                        lblMensaje.Text = "ID de producto inválido.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    // Si no se pasó un ID, muestra un mensaje de error.
                    lblMensaje.Text = "No se especificó un producto para editar.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void CargarProducto(int productoID)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    // Consulta SELECT que incluye la nueva columna 'Codigo'
                    string query = "SELECT ProductoID, Codigo, NombreProducto, Descripcion, Precio, Stock, Categoria FROM Productos WHERE ProductoID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", productoID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Llena los campos del formulario con los datos de la base de datos.
                                hdnProductoID.Value = reader["ProductoID"].ToString();
                                txtCodigo.Text = reader["Codigo"].ToString();
                                txtNombre.Text = reader["NombreProducto"].ToString();
                                txtDescripcion.Text = reader["Descripcion"].ToString();
                                txtPrecio.Text = reader["Precio"].ToString();
                                txtStock.Text = reader["Stock"].ToString();

                                // Si tienes un campo para la categoría, asegúrate de cargarlo.
                                // txtCategoria.Text = reader["Categoria"].ToString();
                            }
                            else
                            {
                                lblMensaje.Text = "Producto no encontrado.";
                                lblMensaje.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los datos del producto: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Usa el HiddenField para obtener el ID del producto
                int productoID = Convert.ToInt32(hdnProductoID.Value);
                string codigo = txtCodigo.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                decimal precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                int stock = Convert.ToInt32(txtStock.Text.Trim());

                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    // Consulta UPDATE para guardar los cambios, incluyendo la columna 'Codigo'
                    string query = "UPDATE Productos SET Codigo = @Codigo, NombreProducto = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock WHERE ProductoID = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Codigo", codigo);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@Precio", precio);
                        cmd.Parameters.AddWithValue("@Stock", stock);
                        cmd.Parameters.AddWithValue("@ID", productoID);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            lblMensaje.Text = "Producto actualizado correctamente.";
                            lblMensaje.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMensaje.Text = "No se pudo actualizar el producto. Es posible que no se haya encontrado.";
                            lblMensaje.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (FormatException)
            {
                lblMensaje.Text = "Error: El precio y la cantidad deben ser números válidos.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar los cambios: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}