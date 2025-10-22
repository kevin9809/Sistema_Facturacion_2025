using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages.Inventory
{
    public partial class EliminarInventario : System.Web.UI.Page
    {
        ConexionDB conexion = new ConexionDB();
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
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
                    string query = "SELECT ProductoID, NombreProducto, Descripcion, Precio, Stock, Categoria FROM Productos WHERE ProductoID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", productoID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Llena los campos del formulario con los datos de la base de datos.
                                lbId.Text = reader["ProductoID"].ToString();
                                lbNombre.Text = reader["NombreProducto"].ToString();
                                lbDescripcion.Text = reader["Descripcion"].ToString();
                                lbPrecio.Text = reader["Precio"].ToString();
                                lbCantidad.Text = reader["Stock"].ToString();

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

        protected void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            int productoID;
            int.TryParse(Request.QueryString["id"], out productoID);

            EliminarProducto(productoID);
        }
    }
}