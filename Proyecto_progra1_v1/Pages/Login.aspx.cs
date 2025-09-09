using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient; // Necesario para trabajar con SQL Server
using System.Configuration; // Necesario para leer la cadena de conexión

namespace Proyecto_progra1_v1.Pages
{
    // Clase de conexión a la base de datos.
    // Lo más recomendado es que esta clase esté en un archivo separado (ConexionDB.cs).
    // Si la pones aquí, debe estar fuera de la clase 'Login' pero dentro del namespace.
    public class ConexionDB
    {
        private string connectionString;

        public ConexionDB()
        {
            // Obtiene la cadena de conexión desde el archivo Web.config.
            // Asegúrate de que tu Web.config tenga un <connectionStrings> con el nombre "InventarioDB".
            connectionString = ConfigurationManager.ConnectionStrings["InventarioDB"].ConnectionString;
        }

        public SqlConnection Conectar()
        {
            return new SqlConnection(connectionString);
        }
    }

    public partial class Login : System.Web.UI.Page
    {
        // Instancia de la clase de conexión a la BD.
        ConexionDB conexion = new ConexionDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            // El Page_Load se ejecuta cada vez que la página carga.
            // Para la lógica de base de datos, es mejor usar los eventos de los botones.
        }

        protected void btnMostrarCrearUsuario_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
            pnlCrearUsuario.Visible = true;
            // Limpia los campos y mensajes al cambiar de panel
            LimpiarCamposLogin();
            LimpiarCamposCrearUsuario();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            pnlCrearUsuario.Visible = false;
            pnlLogin.Visible = true;
            // Limpia los campos y mensajes al volver al panel de login
            LimpiarCamposLogin();
            LimpiarCamposCrearUsuario();
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                lblMensaje.Text = "Por favor, ingrese usuario y contraseña.";
                return;
            }

            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Nombre = @Nombre AND Contraseña = @Contrasena";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            lblMensaje.Text = "¡Inicio de sesión exitoso!";
                            // Redirige al usuario a la página principal.
                            Response.Redirect("Home.aspx");
                        }
                        else
                        {
                            lblMensaje.Text = "Usuario o contraseña incorrectos.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al iniciar sesión: " + ex.Message;
            }
        }

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtNuevoUsuario.Text.Trim();
            string contrasena = txtNuevoUsuario0.Text.Trim();
            string confirmarContrasena = txtNuevoUsuario1.Text.Trim();
            string email = txtNuevoUsuario2.Text.Trim();

            if (contrasena != confirmarContrasena)
            {
                lblMensaje.Text = "Las contraseñas no coinciden.";
                return;
            }

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(email))
            {
                lblMensaje.Text = "Todos los campos son obligatorios.";
                return;
            }

            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "INSERT INTO Usuarios (Nombre, Contraseña, Email, Rol) VALUES (@Nombre, @Contrasena, @Email, @Rol)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Rol", "cliente");

                        cmd.ExecuteNonQuery();
                    }
                }
                lblMensaje.Text = "Usuario creado exitosamente. ¡Ahora puedes iniciar sesión!";
                pnlCrearUsuario.Visible = false;
                pnlLogin.Visible = true;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Código de error de duplicado en SQL Server
                {
                    lblMensaje.Text = "El nombre de usuario o correo electrónico ya existe.";
                }
                else
                {
                    lblMensaje.Text = "Error al crear el usuario: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error inesperado: " + ex.Message;
            }
        }

        // Este método se activa cuando el texto de txtNuevoUsuario1 cambia.
        // Lo dejaremos vacío por ahora, ya que la validación se hace en el clic del botón.
        protected void txtNuevoUsuario1_TextChanged(object sender, EventArgs e)
        {
            // Esta lógica no es necesaria si la validación se hace en el botón de "Crear Usuario".
        }

        // Métodos auxiliares para limpiar campos y mensajes
        private void LimpiarCamposLogin()
        {
            txtUsuario.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            lblMensaje.Text = string.Empty;
        }

        private void LimpiarCamposCrearUsuario()
        {
            txtNuevoUsuario.Text = string.Empty;
            txtNuevoUsuario0.Text = string.Empty;
            txtNuevoUsuario1.Text = string.Empty;
            txtNuevoUsuario2.Text = string.Empty;
            lblMensaje.Text = string.Empty;
        }
    }
}