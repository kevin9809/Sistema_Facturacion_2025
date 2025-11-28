using Microsoft.Reporting.WebForms;
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

                var datos = db.Clientes.ToList();
                    //.Where(c => c.Fecha >= ini && c.Fecha <= fin)
                    //.ToList();

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reporte/Report.rdlc");

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", datos);
                ReportViewer1.LocalReport.DataSources.Add(rds);

                ReportParameter p = new ReportParameter("inicio", ini.ToShortDateString());
                ReportParameter p2 = new ReportParameter("fin", fin.ToShortDateString());
                ReportViewer1.LocalReport.SetParameters(new[] { p });
                ReportViewer1.LocalReport.SetParameters(new[] { p2 });

                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}