<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditarCliente.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Client.ActualizarCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <h2 class="mb-4">Editar Cliente</h2>

        <div class="border p-4 rounded shadow-sm bg-light">
            <div class="mb-3">
                <asp:Label ID="lbNombre" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lbDireccion" runat="server" Text="Dirección" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lbTelefono" runat="server" Text="Teléfono" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTelefono" runat="server" TextMode="Phone" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lbEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Button ID="btnEditarCliente" runat="server" Text="Editar" CssClass="btn btn-primary" />
            <a href="Clientes.aspx" class="btn btn-secondary">Cancelar</a>
        </div>
    </div>

</asp:Content>
