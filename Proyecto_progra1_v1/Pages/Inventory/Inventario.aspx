<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">
        <h2>Inventario</h2>
        <asp:Button ID="btnAgregarInventario" runat="server" Text="Agregar a inventario" />
        <asp:GridView ID="dgvInventario" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="true"></asp:GridView>
        
    </form>
    
</asp:Content>
