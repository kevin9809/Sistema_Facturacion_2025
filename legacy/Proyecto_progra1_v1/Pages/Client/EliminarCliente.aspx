<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EliminarCliente.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Client.EliminarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-4" style="max-width: 600px">
        <h2 style="color: crimson">Eliminar Cliente</h2>

        <asp:HiddenField ID="hdnIdCliente" runat="server" />

        <div class="card">
            <div class="card" role="alert" style="background-color: moccasin">
                ¿Está seguro de que desea eliminar este cliente? Esta acción no se puede deshacer.
            </div>

            <table>
                <tr>
                    <td><b>Nombre</b></td>
                    <td>
                        <asp:Label ID="lbNombre" runat="server" Text="Alfredo Navarro"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Dirección</b></td>
                    <td>
                        <asp:Label ID="lbDireccion" runat="server" Text="Colonia las américas, Nejapa"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Teléfono</b></td>
                    <td>
                        <asp:Label ID="lbTelefono" runat="server" Text="78937854"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Email</b></td>
                    <td>
                        <asp:Label ID="lbEmail" runat="server" Text="freddy.navarro.g10@gmail.com"></asp:Label>
                    </td>
                </tr>
            </table>

            <div>
                <asp:Button ID="btnEliminarCliente" runat="server" Text="Eliminar" CssClass="buttonRed" OnClick="btnEliminarCliente_Click"/>
                <asp:Button ID="btnCancelarCliente" runat="server" Text="Volver" CssClass="buttonGray" CausesValidation="False" PostBackUrl="~/Pages/Client/Clientes.aspx" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

        </div>
    </div>
</asp:Content>
