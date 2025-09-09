<%@ Page Title="Inventario" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <h2 class="mb-4">Gestión de Inventario</h2>

        <asp:Button ID="btnAgregarNuevo" runat="server" Text="Agregar a inventario" OnClick="btnAgregarNuevo_Click" CssClass="btn btn-primary mb-3" />

        <asp:GridView ID="dgvInventario" runat="server" 
            AutoGenerateColumns="false"
            CssClass="table table-bordered table-striped table-hover"
            OnRowCommand="dgvInventario_RowCommand">

            <Columns>
                <asp:BoundField DataField="ProductoID" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoría" />

                <asp:TemplateField HeaderText="Funciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditar" runat="server" 
                            Text="Editar" 
                            CommandName="EditarProducto" 
                            CommandArgument='<%# Eval("ProductoID") %>' 
                            CssClass="btn btn-primary btn-sm me-2" />
                        
                        <asp:LinkButton ID="btnEliminar" runat="server" 
                            Text="Eliminar" 
                            CommandName="EliminarProducto" 
                            CommandArgument='<%# Eval("ProductoID") %>' 
                            OnClientClick="return confirm('¿Está seguro de que desea eliminar este producto?');" 
                            CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
    </div>
</asp:Content>