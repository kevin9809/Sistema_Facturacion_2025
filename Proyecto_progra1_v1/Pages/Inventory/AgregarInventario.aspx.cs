using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Proyecto_progra1_v1.Pages.Inventory
{
    // Asumiendo que la clase ConexionDB está en un archivo separado.
    public partial class AgregarInventario : System.Web.UI.Page
    {
        // Instancia de tu clase de conexión a la BD
        ConexionDB conexion = new ConexionDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        // Método que se activará al hacer clic en el botón "Agregar"
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Obtiene los valores de los TextBox.
                // Notar que el ID del control de cantidad/stock se ajustó en la respuesta anterior
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                decimal precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                int stock = Convert.ToInt32(txtStock.Text.Trim());

                // Si tienes un campo para el código y la categoría en el HTML, obténlos aquí
                // string codigo = txtCodigo.Text.Trim();
                // string categoria = txtCategoria.Text.Trim();
                // Si no, puedes dejar los campos vacíos o usar valores predeterminados
                string categoria = "General";

                // 2. Lógica para guardar en la base de datos.
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();

                    // La consulta INSERT debe coincidir con los campos que tienes en tu tabla
                    string query = "INSERT INTO Productos (NombreProducto, Descripcion, Precio, Stock, Categoria) VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Categoria)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@Precio", precio);
                        cmd.Parameters.AddWithValue("@Stock", stock);
                        cmd.Parameters.AddWithValue("@Categoria", categoria); // Asegúrate de tener este campo en tu tabla o ajusta la consulta

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            lblMensaje.Text = "¡Producto agregado exitosamente!";
                            lblMensaje.ForeColor = System.Drawing.Color.Green;
                            LimpiarCampos();
                        }
                        else
                        {
                            lblMensaje.Text = "No se pudo agregar el producto.";
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
                lblMensaje.Text = "Error al guardar el producto: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Método para limpiar los campos del formulario
        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            // Si tienes un TextBox para la categoría y el código, límpialos también.
            // txtCodigo.Text = string.Empty;
            // txtCategoria.Text = string.Empty;
        }

        // Si tienes un botón para volver al inventario, este sería el código
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inventario.aspx");
        }
    }
}