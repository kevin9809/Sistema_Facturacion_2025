<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <h2 class="mb-4">Inventario</h2>

            <a href="AgregarInventario.aspx" class="btn btn-primary mb-3">Agregar a inventario</a>

            <asp:GridView ID="dgvInventario" runat="server" AutoGenerateColumns="false"
            CssClass="table table-bordered table-striped table-hover">

             <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />

                <asp:TemplateField HeaderText="Funciones">
                    <ItemTemplate>
                    <a href='<%# "EditarInventario.aspx?id=" + Eval("ID") %>' class="text-primary me-2">Editar</a>
                    <a href='<%# "EliminarInventario.aspx?id=" + Eval("ID") %>' class="text-danger">Eliminar</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            </asp:GridView>
    


    </div>
    
</asp:Content>
