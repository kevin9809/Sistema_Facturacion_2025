<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditarInventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventory.EditarInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div class="container mt-4">
    <h2 class="mb-4">Editar producto</h2>

    <div class="border p-4 rounded shadow-sm bg-light">

        <div class="mb-3">
            <asp:Label ID="lbCodigo" runat="server" Text="Código" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lbNombre" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lbDescripcion" runat="server" Text="Descripción" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lbPrecio" runat="server" Text="Precio" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" step="0.01" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <asp:Label ID="lbCantidad" runat="server" Text="Cantidad" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
        </div>

        <asp:Button ID="btnAgregarInventario" runat="server" Text="Editar" CssClass="btn btn-primary" />
        <a href="Inventario.aspx" class="btn btn-secondary">Cancelar</a>
    </div>

</div>

</asp:Content>
