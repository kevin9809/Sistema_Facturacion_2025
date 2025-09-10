<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EliminarInventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventory.EliminarInventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mb-4" style="max-width: 600px">
        <h2 style="color: crimson">Eliminar de Inventario</h2>

        <div class="card">
            <div class="card" role="alert" style="background-color: moccasin">
                ¿Está seguro de que desea eliminar este producto? Esta acción no se puede deshacer.
            </div>

            <table>
                <tr>
                    <td><b>ID</b></td>
                    <td>
                        <asp:Label ID="lbId" runat="server" Text="01"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Nombre</b></td>
                    <td>
                        <asp:Label ID="lbNombre" runat="server" Text="Mesa acrílica"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Descripción</b></td>
                    <td>
                        <asp:Label ID="lbDescripcion" runat="server" Text="Mesa color blanca medidas 30x20 pulg"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Precio</b></td>
                    <td>
                        <asp:Label ID="lbPrecio" runat="server" Text="105.75"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Cantidad</b></td>
                    <td>
                        <asp:Label ID="lbCantidad" runat="server" Text="2"></asp:Label>
                    </td>
                </tr>
            </table>

            <div>
                <asp:Button ID="btnEliminarCliente" runat="server" Text="Eliminar" CssClass="buttonRed" OnClick="btnEliminarCliente_Click" />
                <asp:Button ID="btnCancelarCliente" runat="server" Text="Volver" CssClass="buttonGray" CausesValidation="False" PostBackUrl="~/Pages/Inventory/Inventario.aspx" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

        </div>
    </div>

</asp:Content>
