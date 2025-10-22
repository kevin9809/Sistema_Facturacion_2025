using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages.Client
{
    public partial class EliminarCliente : System.Web.UI.Page
    {
        ConexionDB conexion = new ConexionDB();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica si se pasó un ID de cliente en la URL
                if (Request.QueryString["id"] != null)
                {
                    int clienteID = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDatosCliente(clienteID);
                }
                else
                {
                    // Si no hay ID, redirige a la página principal de clientes
                    Response.Redirect("Clientes.aspx");
                }
            }
        }

        private void CargarDatosCliente(int clienteID)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "SELECT Nombre, Direccion, Telefono, Email FROM Clientes WHERE ID_Cliente = @ID_Cliente";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID_Cliente", clienteID);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Rellenar los campos del formulario con los datos del cliente
                            lbNombre.Text = reader["Nombre"].ToString();
                            lbDireccion.Text = reader["Direccion"].ToString();
                            lbTelefono.Text = reader["Telefono"].ToString();
                            lbEmail.Text = reader["Email"].ToString();

                            // Guardar el ID en el HiddenField para el PostBack
                            hdnIdCliente.Value = clienteID.ToString();
                        }
                        else
                        {
                            // Si no se encuentra el cliente, mostrar un mensaje de error y redirigir
                            lblMensaje.Text = "No se encontró el cliente.";
                            Response.Redirect("Clientes.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los datos del cliente: " + ex.Message;
            }
        }

        private void EliminarClienteBD(int clienteID)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "DELETE FROM Clientes WHERE ID_Cliente = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", clienteID);
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            lblMensaje.Text = "Cliente eliminado correctamente.";
                            lblMensaje.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMensaje.Text = "No se pudo eliminar el cliente.";
                            lblMensaje.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el cliente: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            int clienteID = Convert.ToInt32(Request.QueryString["id"]);
            EliminarClienteBD(clienteID);
        }
    }
}