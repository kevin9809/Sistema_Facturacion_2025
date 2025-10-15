<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mb-4" style="width: 500px">
        <asp:Panel ID="pnlLogin" runat="server">

            <div class="card">
                <h2 style="text-align: center; margin-top: 2rem; margin-bottom: 2rem">Iniciar sesión</h2>

                <div class="validadores">
                    <asp:Label ID="lblusuario" runat="server" Text="Nombre de usuario" />
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Campo requerido" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </div>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="textBoxs" Style="margin-bottom: 1rem"></asp:TextBox>

                <div class="validadores">
                    <asp:Label ID="lblContrasena" runat="server" Text="Contraseña" />
                    <asp:RequiredFieldValidator ID="rfvContra" runat="server" ControlToValidate="txtContrasena" ErrorMessage="Campo requerido" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </div>
                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="textBoxs"></asp:TextBox>

                <div style="text-align: center; display: flex; flex-direction: column; margin-top:2rem; margin-bottom:2rem">
                    <asp:Button ID="btnIniciarSesion" runat="server" OnClick="btnIniciarSesion_Click" Text="Iniciar Sesión" CssClass="buttonBlue" />
                    <asp:LinkButton ID="hlCrearUsuario" OnClick="btnMostrarCrearUsuario_Click" runat="server" CausesValidation="False">Crear usuario</asp:LinkButton>
                </div>

            </div>

        </asp:Panel>

        <asp:Panel ID="pnlCrearUsuario" runat="server" Visible="false">

            <div class="card">
                <h2>Crear usuario</h2>

                <div class="validadores">
                    <asp:Label ID="lblUsuarioNuevo" runat="server" Text="Nombre de usuario" AssociatedControlID="txtNuevoUsuario" CssClass="form-label" />
                    <asp:RequiredFieldValidator ID="rfvUsuarioNuevo" runat="server" ControlToValidate="txtNuevoUsuario" ErrorMessage="Campo requerido" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </div>
                <asp:TextBox ID="txtNuevoUsuario" runat="server" CssClass="textBoxs"/>

                <div class="validadores">
                    <asp:Label ID="lblContrasenaNueva" runat="server" Text="Contraseña" AssociatedControlID="txtNuevoUsuario0" CssClass="form-label" />
                    <asp:RequiredFieldValidator ID="rfvContraNueva" runat="server" ControlToValidate="txtNuevoUsuario0" ErrorMessage="Campo requerido" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </div>                
                <asp:TextBox ID="txtNuevoUsuario0" runat="server" TextMode="Password" CssClass="textBoxs" />

                <div class="validadores">
                    <asp:Label ID="lblConfirmarContra" runat="server" Text="Confirmar contraseña" AssociatedControlID="txtNuevoUsuario1" CssClass="form-label" />
                    <asp:CompareValidator ID="cvConfirmarContra" runat="server" ErrorMessage="La contraseña no coincide" ControlToCompare="txtNuevoUsuario0" ControlToValidate="txtNuevoUsuario1" ForeColor="#CC0000"></asp:CompareValidator>
                </div>
                <asp:TextBox ID="txtNuevoUsuario1" runat="server" TextMode="Password" CssClass="textBoxs" />

                <div class="validadores">
                    <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico"/>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Correo inválido" ForeColor="#CC0000" ControlToValidate="txtNuevoCorreo" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </div>
                <asp:TextBox ID="txtNuevoCorreo" runat="server" CssClass="textBoxs" />

                <div>
                    <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear usuario" CssClass="buttonGreen" OnClick="btnCrearUsuario_Click"/>
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="buttonGray" OnClick="btnVolver_Click" CausesValidation="False"/>
                </div>

            </div>
        </asp:Panel>
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" style="text-align:center;"/>
    </div>
</asp:Content>
