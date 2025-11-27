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
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reporte/Report.rdlc");

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", db.Clientes.ToList());
                ReportViewer1.LocalReport.DataSources.Add(rds);

                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}