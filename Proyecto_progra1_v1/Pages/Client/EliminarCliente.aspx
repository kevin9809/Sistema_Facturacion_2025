<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EliminarCliente.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Client.EliminarCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <h2>Eliminar Cliente</h2>

    <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>
    <asp:Label ID="lbNombre" runat="server" Text="Alfredo Navarro"></asp:Label>
    
    <asp:Label ID="Label2" runat="server" Text="Dirección"></asp:Label>
    <asp:Label ID="lbDireccion" runat="server" Text="Colonia las américas, Nejapa"></asp:Label>
    
    <asp:Label ID="Label3" runat="server" Text="Teléfono"></asp:Label>
    <asp:Label ID="lbTelefono" runat="server" Text="78937854"></asp:Label>
    
    <asp:Label ID="Label4" runat="server" Text="Email"></asp:Label>
    <asp:Label ID="lbEmail" runat="server" Text="freddy.navarro.g10@gmail.com"></asp:Label>
    
    <asp:Button ID="btnEliminarCliente" runat="server" Text="Eliminar" />
    </form>
</asp:Content>
