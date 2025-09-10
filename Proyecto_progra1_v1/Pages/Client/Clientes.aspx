<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Clientes registrados</h2>

        <asp:Button ID="btnRegistrarCliente" runat="server" Text="Agregar Cliente" PostBackUrl="~/Pages/Client/RegistrarCliente.aspx" CssClass="buttonBlue" />

        <asp:GridView ID="dgvClientes" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" CssClass="tableStyle">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID_Cliente" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="ID_Usuario" HeaderText="ID de Usuario" />

                <asp:TemplateField HeaderText="Funciones">
                    <ItemTemplate>
                        <a href='<%# "EditarCliente.aspx?id=" + Eval("ID_Cliente") %>' class="text-primary me-2">Editar</a>
                        <a href='<%# "EliminarCliente.aspx?id=" + Eval("ID_Cliente") %>' class="text-danger">Eliminar</a>
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

        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />

    </div>
</asp:Content>
