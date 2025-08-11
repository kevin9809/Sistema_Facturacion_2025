<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AgregarInventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventory.AgregarInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">
    <h2>Agregar a Inventario</h2>
    <asp:Label ID="lbCodigo" runat="server" Text="Código"></asp:Label>
    <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>

    <asp:Label ID="lbNombre" runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>

    <asp:Label ID="lbDescripcion" runat="server" Text="Descripción"></asp:Label>
    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>

    <asp:Label ID="lbPrecio" runat="server" Text="Precio"></asp:Label>
    <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" step="0.01"></asp:TextBox>

    <asp:Label ID="lbCantidad" runat="server" Text="Cantidad"></asp:Label>
    <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number"></asp:TextBox>

    <asp:Button ID="btnAgregarInventario" runat="server" Text="Agregar" />
    </form>

</asp:Content>
