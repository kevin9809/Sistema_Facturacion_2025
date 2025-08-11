using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dgvClientes.DataSource = DatosTemporales();
                dgvClientes.DataBind();
            }
        }
        private DataTable DatosTemporales()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Direccion", typeof(string));
            dt.Columns.Add("Teléfono", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Funciones");

            string funciones = "Editar | Eliminar";

            dt.Rows.Add(1, "Juan Pérez", "Av. Central #123", "555-1234", "juan@example.com", funciones);
            dt.Rows.Add(2, "María López", "Calle 5 #45", "555-5678", "maria@example.com", funciones);
            dt.Rows.Add(3, "Carlos Gómez", "Boulevard Norte #77", "555-9999", "carlos@example.com", funciones);
            dt.Rows.Add(4, "Ana Torres", "Col. Jardines #15", "555-2222", "ana@example.com", funciones);

            return dt;
        }
    }
}