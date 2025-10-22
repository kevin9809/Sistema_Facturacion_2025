using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages.Client
{
    public partial class ActualizarCliente : System.Web.UI.Page
    {
        ConexionDB conexion = new ConexionDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
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
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtDireccion.Text = reader["Direccion"].ToString();
                            txtTelefono.Text = reader["Telefono"].ToString();
                            txtEmail.Text = reader["Email"].ToString();

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

        protected void btnEditarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                int clienteID = Convert.ToInt32(hdnIdCliente.Value);
                string nombre = txtNombre.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string email = txtEmail.Text.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(email))
                {
                    lblMensaje.Text = "Por favor, complete todos los campos.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "UPDATE Clientes SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono, Email = @Email WHERE ID_Cliente = @ID_Cliente";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Direccion", direccion);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@ID_Cliente", clienteID);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            lblMensaje.Text = "Cliente actualizado correctamente.";
                            lblMensaje.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMensaje.Text = "No se pudo actualizar el cliente.";
                            lblMensaje.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar el cliente: " + ex.Message;
            }
        }
    }
}