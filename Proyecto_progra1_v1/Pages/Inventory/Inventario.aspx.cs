using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Proyecto_progra1_v1.Pages
{
    public partial class Inventario : System.Web.UI.Page
    {
        // Instancia de tu clase de conexión a la BD
        // Asume que la clase ConexionDB está en un archivo separado
        ConexionDB conexion = new ConexionDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Solo carga los datos la primera vez que la página carga
            if (!IsPostBack)
            {
                CargarInventario();
            }
        }

        // **Este método reemplaza a DatosTemporales()**
        // Carga los productos desde la base de datos y los muestra en el GridView
        private void CargarInventario()
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    // Consulta SELECT para obtener los datos de la tabla Productos
                    string query = "SELECT ProductoID, NombreProducto, Descripcion, Precio, Stock, Categoria FROM Productos ORDER BY NombreProducto";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Asigna los datos al GridView
                        dgvInventario.DataSource = dt;
                        dgvInventario.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si la conexión o consulta falla
                lblMensaje.Text = "Error al cargar el inventario: " + ex.Message;
            }
        }

        // Método que se activa al hacer clic en el botón "Agregar a inventario"
        protected void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarInventario.aspx");
        }

        // Maneja los eventos de los botones "Editar" y "Eliminar" en el GridView
        protected void dgvInventario_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int productoID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EliminarProducto")
            {
                EliminarProducto(productoID);
                CargarInventario(); // Recarga el GridView después de la eliminación
            }
            else if (e.CommandName == "EditarProducto")
            {
                Response.Redirect("EditarInventario.aspx?id=" + productoID);
            }
        }

        // Lógica para eliminar un producto de la base de datos
        private void EliminarProducto(int productoID)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "DELETE FROM Productos WHERE ProductoID = @ProductoID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", productoID);
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            lblMensaje.Text = "Producto eliminado correctamente.";
                        }
                        else
                        {
                            lblMensaje.Text = "No se encontró el producto para eliminar.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el producto: " + ex.Message;
            }
        }
    }
}