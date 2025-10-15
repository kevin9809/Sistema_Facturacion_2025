﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages
{
    public partial class Clientes : System.Web.UI.Page
    {
        // Instancia de tu clase de conexión a la BD
        ConexionDB conexion = new ConexionDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
        }

        private void CargarClientes()
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    // La consulta SELECT debe usar los nombres de columna correctos de tu base de datos
                    string query = "SELECT ID_Cliente, Nombre, Direccion, Telefono, Email, ID_Usuario FROM Clientes";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvClientes.DataSource = dt;
                        dgvClientes.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los clientes: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarCliente.aspx");
        }

        protected void dgvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int clienteID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditarCliente")
            {
                Response.Redirect("EditarCliente.aspx?id=" + clienteID);
            }
        }
    }
}