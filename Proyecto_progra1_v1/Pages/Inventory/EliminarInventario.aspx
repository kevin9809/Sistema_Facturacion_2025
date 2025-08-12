<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EliminarInventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventory.EliminarInventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <h2 class="mb-4 text-danger">Eliminar de Inventario</h2>
        <form runat="server" class="border p-4 rounded bg-light shadow-sm">
            <div class="alert alert-warning" role="alert">
                ¿Está seguro de que desea eliminar este producto? Esta acción no se puede deshacer.
            </div>

            <dl class="row">
                <dt class="col-sm-3">ID</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="ID" runat="server" Text="01"></asp:Label>
                </dd>

                <dt class="col-sm-3">Código</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="lbCodigo" runat="server" Text="MA01"></asp:Label>
                </dd>

                <dt class="col-sm-3">Nombre</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="lbNombre" runat="server" Text="Mesa acrílica"></asp:Label>
                </dd>

                <dt class="col-sm-3">Descripción</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="lbDescripcion" runat="server" Text="Mesa color blanca medidas 30x20 pulg"></asp:Label>
                </dd>

                <dt class="col-sm-3">Precio</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="lbPrecio" runat="server" Text="105.75"></asp:Label>
                </dd>

                <dt class="col-sm-3">Cantidad</dt>
                <dd class="col-sm-9">
                    <asp:Label ID="lbCantidad" runat="server" Text="2"></asp:Label>
                </dd>
            </dl>

            <asp:Button ID="btnEliminarInventario" runat="server" Text="Eliminar" CssClass="btn btn-danger"/>
            <a href="Inventario.aspx" class="btn btn-secondary">Cancelar</a>
        </form>
    </div>

</asp:Content>
