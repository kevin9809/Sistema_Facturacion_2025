<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Inventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <h2>Inventario</h2>
        <asp:Button ID="btnAgregarInventario" runat="server" Text="Agregar a inventario" CssClass="buttonBlue" PostBackUrl="~/Pages/Inventory/AgregarInventario.aspx" />

        <asp:GridView ID="dgvInventario" runat="server" AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" CssClass="tableStyle">

            <AlternatingRowStyle BackColor="White" />

            <Columns>
                <asp:BoundField DataField="ProductoID" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoría" />

                <asp:TemplateField HeaderText="Funciones">
                    <ItemTemplate>
                        <a href='<%# "EditarInventario.aspx?id=" + Eval("ProductoID") %>' class="text-primary me-2">Editar</a>
                        <a href='<%# "EliminarInventario.aspx?id=" + Eval("ProductoID") %>' class="text-danger">Eliminar</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />

        </asp:GridView>

        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

    </div>

</asp:Content>
