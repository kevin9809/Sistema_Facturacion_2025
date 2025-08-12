using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_progra1_v1.Pages
{
    public partial class Facturas : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
        protected void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            // Oculta el panel principal y muestra el de nueva factura
            pnlOpcionesFactura.Visible = false;
            pnlVerFactura.Visible = false;
            pnlNuevaFactura.Visible = true;
        }

        protected void btnVerFactura_Click(object sender, EventArgs e)
        {
            // Oculta el panel principal y muestra el de ver factura
            pnlOpcionesFactura.Visible = false;
            pnlNuevaFactura.Visible = false;
            pnlVerFactura.Visible = true;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Oculta los paneles de formularios y muestra el panel principal
            pnlNuevaFactura.Visible = false;
            pnlVerFactura.Visible = false;
            pnlOpcionesFactura.Visible = true;
        }

       
        protected void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            // Lógica para guardar la factura
        }

        protected void btnBuscarFactura_Click(object sender, EventArgs e)
        {
            // Lógica para buscar la factura
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
 
            if (!IsPostBack)
            {

                LlenarTablaDeFactura();
            }
        }

        private void LlenarTablaDeFactura()
        {
          
            var productosDeFactura = new List<dynamic>();

           
            productosDeFactura.Add(new { ID = 1, Descripcion = "Silla", Precio = 50, Cantidad = 2, Total = 100 });
            productosDeFactura.Add(new { ID = 2, Descripcion = "Mesa", Precio = 120, Cantidad = 1, Total = 120 });

           
            gvFacturaDetalles.DataSource = productosDeFactura;

            gvFacturaDetalles.DataBind();
        }
    }


}