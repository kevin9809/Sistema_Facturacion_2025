<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteViewer.aspx.cs" Inherits="Proyecto_MVC.Views.Reporte.ReporteViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms"
    Namespace="Microsoft.Reporting.WebForms"
    TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <a href="/Facturas/Reporte" class="btn btn-primary" 
   style="margin-bottom: 15px; display:inline-block; font-family:Arial; font-size:12px">
    ← Volver a Reporte Facturas
</a>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <rsweb:reportviewer
                id="ReportViewer1"
                runat="server"
                width="100%"
                height="900px"
                processingmode="Local">
            </rsweb:reportviewer>
        </div>
    </form>
</body>
</html>
