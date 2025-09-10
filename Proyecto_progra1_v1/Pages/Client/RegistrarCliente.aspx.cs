using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages.Client
{
    public partial class RegistrarCliente : System.Web.UI.Page
    {
        // Instancia de tu clase de conexión a la BD
        ConexionDB conexion = new ConexionDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string email = txtEmail.Text.Trim();
                
                // Asegurarse de que el campo ID_Usuario no esté vacío
                int idUsuario;
                if (!int.TryParse(txtIdUsuario.Text.Trim(), out idUsuario))
                {
                    lblMensaje.Text = "El ID de Usuario debe ser un número válido.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Validar que los campos de texto no estén vacíos
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(email))
                {
                    lblMensaje.Text = "Por favor, complete todos los campos.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();

                    // ¡SOLUCIÓN! Se omite ID_Cliente porque es una columna IDENTITY.
                    // La base de datos asignará el valor automáticamente.
                    string query = "INSERT INTO Clientes (Nombre, Direccion, Telefono, Email, ID_Usuario) VALUES (@Nombre, @Direccion, @Telefono, @Email, @ID_Usuario)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Direccion", direccion);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@ID_Usuario", idUsuario);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            lblMensaje.Text = "¡Cliente registrado exitosamente!";
                            lblMensaje.ForeColor = System.Drawing.Color.Green;
                            LimpiarCampos();
                        }
                        else
                        {
                            lblMensaje.Text = "No se pudo registrar el cliente.";
                            lblMensaje.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar el cliente: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
        
        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtIdUsuario.Text = string.Empty;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clientes.aspx");
        }
    }
}