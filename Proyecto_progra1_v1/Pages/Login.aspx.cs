using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

protected void btnMostrarCrearUsuario_Click(object sender, EventArgs e)
{
    // Oculta el panel de inicio de sesión
    pnlLogin.Visible = false;
    // Muestra el panel de crear usuario
    pnlCrearUsuario.Visible = true;
}

protected void btnVolver_Click(object sender, EventArgs e)
{
    // Oculta el panel de crear usuario
    pnlCrearUsuario.Visible = false;
    // Muestra el panel de inicio de sesión
    pnlLogin.Visible = true;
}

protected void btnIniciarSesion_Click(object sender, EventArgs e)
{
    // Aquí iría la lógica para validar el inicio de sesión
    // Por ahora, puedes dejarlo vacío o con un mensaje de prueba
}

protected void btnCrearUsuario_Click(object sender, EventArgs e)
{
    // Aquí iría la lógica para guardar el nuevo usuario en la base de datos
    // Por ahora, puedes dejarlo vacío o con un mensaje de prueba
}

        protected void txtNuevoUsuario1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}