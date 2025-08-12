using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages
{
    public partial class Inventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                dgvInventario.DataSource = DatosTemporales();
                dgvInventario.DataBind();
            }
        }

        private DataTable DatosTemporales()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Descripcion", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Cantidad", typeof(string));

            dt.Rows.Add(1, "MA01", "Mesa acrílica", "Mesa color blanca medidas 30x20 pulg", "105.75", "2");
            dt.Rows.Add(2, "SI02", "Silla de madera", "Silla de pino barnizada", "45.50", "8");
            dt.Rows.Add(3, "LA03", "Lámpara LED", "Lámpara de escritorio luz fría", "22.99", "5");
            dt.Rows.Add(4, "ES04", "Escritorio", "Escritorio MDF 120x60 cm", "150.00", "3");
            dt.Rows.Add(5, "CA05", "Cama matrimonial", "Cama con base y colchón ortopédico", "350.00", "1");
            dt.Rows.Add(6, "RE06", "Repisa flotante", "Repisa de madera 80 cm", "18.75", "10");

            return dt;
        }

    }
}