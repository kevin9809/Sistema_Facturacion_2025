<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EliminarCliente.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Client.EliminarCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4 text-danger">Eliminar Cliente</h2>

        <form runat="server" class="border p-4 rounded bg-light shadow-sm">
            <div class="alert alert-warning" role="alert">
                ¿Está seguro de que desea eliminar este cliente? Esta acción no se puede deshacer.
            </div>

            <dl class="row">
                <dt class="col-sm-3">Nombre</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="lbNombre" runat="server" Text="Alfredo Navarro"></asp:Label>
                </dd>

                <dt class="col-sm-3">Dirección</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="lbDireccion" runat="server" Text="Colonia las américas, Nejapa"></asp:Label>
                </dd>

                <dt class="col-sm-3">Teléfono</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="lbTelefono" runat="server" Text="78937854"></asp:Label>
                </dd>

                <dt class="col-sm-3">Email</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="lbEmail" runat="server" Text="freddy.navarro.g10@gmail.com"></asp:Label>
                </dd>
            </dl>

            <div class="mt-4">
                <asp:Button ID="btnEliminarCliente" runat="server" Text="Eliminar" CssClass="btn btn-danger me-2" />
                <a href="Clientes.aspx" class="btn btn-secondary">Cancelar</a>
            </div>
        </form>
    </div>
</asp:Content>
