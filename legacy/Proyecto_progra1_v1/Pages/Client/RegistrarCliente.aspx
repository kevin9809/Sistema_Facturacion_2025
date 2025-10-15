<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegistrarCliente.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Client.RegistrarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-4" style="max-width: 600px">
        <h2>Registrar Cliente</h2>

        <div class="card">
            <div class="validadores">
                <asp:Label ID="lbNombre" runat="server" Text="Nombre"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo requerido" ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </div>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="textBoxs"></asp:TextBox>

            <div class="validadores">
                <asp:Label ID="lbDireccion" runat="server" Text="Dirección"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txtDireccion" ErrorMessage="Campo requerido" ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </div>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="textBoxs"></asp:TextBox>

            <div class="validadores">
                <asp:Label ID="lbTelefono" runat="server" Text="Teléfono"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono" ErrorMessage="Campo requerido" ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </div>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="textBoxs"></asp:TextBox>

            <div class="validadores">
                <asp:Label ID="lbEmail" runat="server" Text="Email"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Campo requerido" ForeColor="#CC0000" Visible="True"></asp:RequiredFieldValidator>
            </div>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="textBoxs"></asp:TextBox>

            <div class="validadores">
                <asp:Label ID="lbIdUsuario" runat="server" Text="ID Usuario" CssClass="form-label"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvIdUsuario" runat="server" ControlToValidate="txtIdUsuario" ErrorMessage="Campo requerido" ForeColor="#CC0000" Visible="True"></asp:RequiredFieldValidator>
            </div>
            <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control"></asp:TextBox>

            <div style="text-align: center">
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Correo inválido" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="#CC0000"></asp:RegularExpressionValidator>
            </div>

            <div>
                <asp:Button ID="btnRegistrarCliente" OnClick="btnRegistrar_Click" runat="server" Text="Registrar" CssClass="buttonGreen" />
                <asp:Button ID="btnCancelar" runat="server" Text="Volver" CssClass="buttonGray" PostBackUrl="~/Pages/Client/Clientes.aspx" CausesValidation="False" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

        </div>

    </div>
</asp:Content>
