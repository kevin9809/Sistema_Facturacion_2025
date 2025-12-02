using Microsoft.Reporting.WebForms;
using Proyecto_MVC.Models;
using Proyecto_MVC.Repositorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_MVC.Views.Reporte
{
    public partial class ReporteViewer : System.Web.UI.Page
    {
        readonly AppDbContext db = new AppDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime ini = DateTime.Parse(Request.QueryString["ini"]);
                DateTime fin = DateTime.Parse(Request.QueryString["fin"]);

                var lista = (from f in db.Facturas
                             join c in db.Clientes on f.ClienteID equals c.ID_Cliente
                             join d in db.DetalleFactura on f.FacturaID equals d.FacturaID
                             join pr in db.Productos on d.ProductoID equals pr.ProductoID
                             select new FacturasReport
                             {
                                 Fecha = f.Fecha,
                                 Nombre = c.Nombre,
                                 NombreProducto = pr.NombreProducto,
                                 Cantidad = d.Cantidad,
                                 PrecioUnitario = d.PrecioUnitario,
                                 Subtotal = f.Total
                             }).
                             Where(f => f.Fecha >= ini && f.Fecha <= fin).ToList();

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reporte/Report.rdlc");

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", lista);
                ReportViewer1.LocalReport.DataSources.Add(rds);

                ReportParameter p = new ReportParameter("inicio", ini.ToShortDateString());
                ReportParameter p2 = new ReportParameter("fin", fin.ToShortDateString());
                ReportParameter p3 = new ReportParameter("total", lista.Sum(t => t.Subtotal).ToString());
                ReportParameter p4 = new ReportParameter("usuario", $"Reporte generado por {Session["Usuario"]} {DateTime.Now.ToString()}");

                ReportViewer1.LocalReport.SetParameters(new[] { p });
                ReportViewer1.LocalReport.SetParameters(new[] { p2 });
                ReportViewer1.LocalReport.SetParameters(new[] { p3 });
                ReportViewer1.LocalReport.SetParameters(new[] { p4 });

                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}