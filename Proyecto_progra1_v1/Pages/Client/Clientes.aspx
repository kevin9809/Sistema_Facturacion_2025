<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container mt-4">
        <h2 class="mb-4 py-5">Clientes registrados</h2>
    
      
            <a href="RegistrarCliente.aspx" class="btn btn-primary mb-3">Agregar cliente</a>

            <asp:GridView ID="dgvClientes" runat="server" AutoGenerateColumns="false"
                          CssClass="table table-bordered table-striped table-hover">
                 <Columns>
                     <asp:BoundField DataField="ID" HeaderText="ID" />
                     <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                     <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                     <asp:BoundField DataField="Teléfono" HeaderText="Teléfono" />
                     <asp:BoundField DataField="Email" HeaderText="Email" />

                     <asp:TemplateField HeaderText="Funciones">
                         <ItemTemplate>
                             <a href='<%# "EditarCliente.aspx?id=" + Eval("ID") %>' class="text-primary me-2">Editar</a>
                             <a href='<%# "EliminarCliente.aspx?id=" + Eval("ID") %>' class="text-danger">Eliminar</a>
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
            </asp:GridView>

    </div>
</asp:Content>
