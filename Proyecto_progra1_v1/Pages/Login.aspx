<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #form1 {
            height: 211px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


        <p>
            <asp:Panel ID="pnlLogin" runat="server">
    <h2>Iniciar Sesión</h2>
                <asp:Label ID="lblusuario" runat="server" Text="Nombre de Usuario:"></asp:Label>
                &nbsp;&nbsp;
                <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                <br />
                <br />
                &nbsp;<asp:Label ID="lblContrasena" runat="server" Text="Contraseña: "></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnIniciarSesion" runat="server" OnClick="btnIniciarSesion_Click" Text="Iniciar Sesión" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnMostrarCrearUsuario" runat="server" OnClick="btnMostrarCrearUsuario_Click" Text="Crear Usuario" />
                <br />
</asp:Panel>

<asp:Panel ID="pnlCrearUsuario" runat="server" Visible="false" Height="282px">
    <h2>Crear Usuario</h2>
    <asp:Label ID="lblUsuarioNuevo" runat="server" Text="Nombre de Usuario:"></asp:Label>
    &nbsp;&nbsp;
    <asp:TextBox ID="txtNuevoUsuario" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblContrasenaNueva" runat="server" Text="Contraseña:"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtNuevoUsuario0" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblConfirmarContra" runat="server" Text="Confirmar contraseña:"></asp:Label>
    <asp:TextBox ID="txtNuevoUsuario1" runat="server" OnTextChanged="txtNuevoUsuario1_TextChanged"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico:"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtNuevoUsuario2" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Text="Contraseña y confirmar contraseña deben coincidir."></asp:Label>
    <br />
    <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear Usuario" OnClick="btnCrearUsuario_Click" />
    <asp:Button ID="btnVolver" runat="server" OnClick="btnVolver_Click" Text="Volver" />
    <br />
</asp:Panel>


</asp:Content>
