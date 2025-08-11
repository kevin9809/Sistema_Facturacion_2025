<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegistrarCliente.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Client.RegistrarCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <h2>Registrar Cliente</h2>

    <asp:Label ID="lbNombre" runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>

    <asp:Label ID="lbDireccion" runat="server" Text="Dirección"></asp:Label>
    <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>

    <asp:Label ID="lbTelefono" runat="server" Text="Teléfono"></asp:Label>
    <asp:TextBox ID="txtTelefono" runat="server" TextMode="Phone" ></asp:TextBox>

    <asp:Label ID="lbEmail" runat="server" Text="Email"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>

    <asp:Button ID="btnRegistrarCliente" runat="server" Text="Registrar" />
    </form>
</asp:Content>
