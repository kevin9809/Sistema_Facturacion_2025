<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EliminarInventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventory.EliminarInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <form runat="server">
  <h2>Eliminar de Inventario</h2>
      <div></div>
      <div></div>
    <asp:Label ID="Label1" runat="server" Text="Código:"></asp:Label>
    <asp:Label ID="lbCodigo" runat="server" Text="MA01"></asp:Label>
  
      <div></div>
    <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
    <asp:Label ID="lbNombre" runat="server" Text="Mesa acrílica"></asp:Label>
  
      <div></div>
    <asp:Label ID="Label3" runat="server" Text="Descripción:"></asp:Label>
  <asp:Label ID="lbDescripcion" runat="server" Text="Mesa color blanca medidas 30x20 pulg"></asp:Label>
  
      <div></div>
      <asp:Label ID="Label4" runat="server" Text="Precio:"></asp:Label>
  <asp:Label ID="lbPrecio" runat="server" Text="105.75"></asp:Label>
  
      <div></div>
      <asp:Label ID="Label5" runat="server" Text="Cantidad:"></asp:Label>
  <asp:Label ID="lbCantidad" runat="server" Text="2"></asp:Label>
  
      <div></div>
      <asp:Button ID="btnEliminarInventario" runat="server" Text="Eliminar"/>
  
  </form>

</asp:Content>
