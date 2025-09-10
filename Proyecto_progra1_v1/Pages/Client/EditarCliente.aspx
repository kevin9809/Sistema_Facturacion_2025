<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditarCliente.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Client.ActualizarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mb-4" style="max-width: 600px">

        <h2>Editar Cliente</h2>

        <asp:HiddenField ID="hdnIdCliente" runat="server" />

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
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="textBoxs"></asp:TextBox>
            <div style="text-align: center">
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Correo inválido" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="#CC0000"></asp:RegularExpressionValidator>
            </div>

            <div>
                <asp:Button ID="btnEditarCliente" OnClick="btnEditarCliente_Click" runat="server" Text="Editar" CssClass="buttonBlue" />
                <asp:Button ID="btnCancelarCliente" runat="server" Text="Volver" CssClass="buttonGray" CausesValidation="False" PostBackUrl="~/Pages/Client/Clientes.aspx" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

        </div>
</asp:Content>
