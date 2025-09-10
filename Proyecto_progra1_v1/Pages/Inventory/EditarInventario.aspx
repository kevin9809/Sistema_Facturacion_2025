<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditarInventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventory.EditarInventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mb-4" style="max-width: 600px">
        <h2>Editar producto</h2>

        <asp:HiddenField ID="hdnProductoID" runat="server" />

        <div class="card">

            <div class="validadores">
                <asp:Label ID="lbNombre" runat="server" Text="Nombre"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtNombre" ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </div>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="textBoxs"></asp:TextBox>

            <div class="validadores">
                <asp:Label ID="lbDescripcion" runat="server" Text="Descripción"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtDescripcion" ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </div>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textBoxs"></asp:TextBox>

            <div class="validadores">
                <asp:Label ID="lbPrecio" runat="server" Text="Precio"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ErrorMessage="Campo requerido" ControlToValidate="txtPrecio" ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </div>
            <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" step="0.01" CssClass="textBoxs"></asp:TextBox>

            <div class="validadores" style="margin-top: 1rem;">
                <asp:Label ID="lbCantidad" runat="server" Text="Cantidad"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ErrorMessage="Campo requerido" ForeColor="#CC0000" ControlToValidate="txtStock"></asp:RequiredFieldValidator>
            </div>
            <asp:TextBox ID="txtStock" runat="server" TextMode="Number" CssClass="textBoxs"></asp:TextBox>

            <div>
                <asp:Button ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" Text="Editar" CssClass="buttonBlue" />
                <asp:Button ID="btnCancelarInventario" runat="server" Text="Volver" CssClass="buttonGray" CausesValidation="False" PostBackUrl="~/Pages/Inventory/Inventario.aspx" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

        </div>

    </div>

</asp:Content>
