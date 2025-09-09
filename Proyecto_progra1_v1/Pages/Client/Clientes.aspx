<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Clientes Registrados</h2>
        
        <asp:Button ID="btnAgregarCliente" runat="server" Text="Agregar cliente" OnClick="btnAgregarCliente_Click" CssClass="btn btn-primary mb-3" />

        <asp:GridView ID="dgvClientes" runat="server" 
            AutoGenerateColumns="false"
            CssClass="table table-bordered table-striped table-hover"
            OnRowCommand="dgvClientes_RowCommand">

            <Columns>
                <asp:BoundField DataField="ID_Cliente" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="ID_Usuario" HeaderText="ID de Usuario" />
                
                <asp:TemplateField HeaderText="Funciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditar" runat="server" 
                            Text="Editar" 
                            CommandName="EditarCliente" 
                            CommandArgument='<%# Eval("ID_Cliente") %>' 
                            CssClass="btn btn-primary btn-sm me-2" />
                        
                        <asp:LinkButton ID="btnEliminar" runat="server" 
                            Text="Eliminar" 
                            CommandName="EliminarCliente" 
                            CommandArgument='<%# Eval("ID_Cliente") %>' 
                            OnClientClick="return confirm('¿Está seguro de que desea eliminar este cliente?');" 
                            CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
    </div>
</asp:Content>
