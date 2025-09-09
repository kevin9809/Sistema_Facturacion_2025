<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #form1 {
            height: 211px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="login-wrapper">
    <div class="login-content">

      <asp:Panel ID="pnlLogin" runat="server">
        <h2 class="text-center mb-4">Iniciar Sesión</h2>
        <asp:Label ID="lblusuario" runat="server" Text="Nombre de Usuario:" AssociatedControlID="txtUsuario" CssClass="form-label" />
        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
        
        <asp:Label ID="lblContrasena" runat="server" Text="Contraseña:" AssociatedControlID="txtContrasena" CssClass="form-label" />
        <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control" />

        <div class="d-flex justify-content-between">
          <asp:Button ID="btnIniciarSesion" runat="server" OnClick="btnIniciarSesion_Click" Text="Iniciar Sesión" CssClass="btn btn-primary" />
          <asp:Button ID="btnMostrarCrearUsuario" runat="server" OnClick="btnMostrarCrearUsuario_Click" Text="Crear Usuario" CssClass="btn btn-secondary" />
        </div>
      </asp:Panel>

      <asp:Panel ID="pnlCrearUsuario" runat="server" Visible="false">
        <h2 class="text-center mb-4">Crear Usuario</h2>

        <asp:Label ID="lblUsuarioNuevo" runat="server" Text="Nombre de Usuario:" AssociatedControlID="txtNuevoUsuario" CssClass="form-label" />
        <asp:TextBox ID="txtNuevoUsuario" runat="server" CssClass="form-control" />

        <asp:Label ID="lblContrasenaNueva" runat="server" Text="Contraseña:" AssociatedControlID="txtNuevoUsuario0" CssClass="form-label" />
        <asp:TextBox ID="txtNuevoUsuario0" runat="server" TextMode="Password" CssClass="form-control" />

        <asp:Label ID="lblConfirmarContra" runat="server" Text="Confirmar contraseña:" AssociatedControlID="txtNuevoUsuario1" CssClass="form-label" />
        <asp:TextBox ID="txtNuevoUsuario1" runat="server" TextMode="Password" CssClass="form-control" />

        <asp:Label ID="lblEmail" runat="server" Text="Correo electrónico:" AssociatedControlID="txtNuevoUsuario2" CssClass="form-label" />
        <asp:TextBox ID="txtNuevoUsuario2" runat="server" CssClass="form-control" />

        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="mt-2 mb-3" />

        <div class="d-flex justify-content-between">
          <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear Usuario" CssClass="btn btn-success" OnClick="btnCrearUsuario_Click" />
          <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn my-btn" OnClick="btnVolver_Click" />
        </div>
      </asp:Panel>

    </div>
  </div>
</asp:Content>