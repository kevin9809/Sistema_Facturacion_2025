<%@ Page Title="Gestión de Facturas" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Facturas.aspx.cs" Inherits="Proyecto_progra1_v1.Pages.Facturas.Facturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-group {
            margin-bottom: 1rem;
        }
        .invoice-header, .invoice-details {
            display: flex;
            justify-content: space-between;
        }
        .product-section, .totals-section {
            margin-top: 2rem;
            border-top: 1px solid #dee2e6;
            padding-top: 1rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">

        <asp:Panel ID="pnlOpcionesFactura" runat="server" CssClass="d-flex justify-content-center mb-4">
            <h2 class="me-4">Gestión de Facturas</h2>
            <asp:Button ID="btnNuevaFactura" runat="server" Text="Crear Nueva Factura" CssClass="btn btn-primary me-2" OnClick="btnNuevaFactura_Click" />
            <asp:Button ID="btnVerFactura" runat="server" Text="Buscar Factura Existente" CssClass="btn btn-secondary" OnClick="btnVerFactura_Click" />
        </asp:Panel>
        
        <asp:Panel ID="pnlNuevaFactura" runat="server" Visible="false" CssClass="p-4 border rounded shadow-sm bg-light">
            <h2>Crear Nueva Factura</h2>
            <hr />

            <div class="invoice-header">
                <div>
                    <img src="../Imagenes/Logo.png" alt="Logo de la Empresa" style="width: 49px; height: 42px;" />
                    <asp:Label ID="lblNombreEmpresa" runat="server" Text="Insight Furniture" CssClass="ms-2 fs-5"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblNumFactura" runat="server" Text="N° Factura:"></asp:Label>
                    <asp:Label ID="lblNumeroFacturaGenerado" runat="server" Text="[Autogenerado]" Font-Bold="true"></asp:Label>
                </div>
            </div>

            <div class="invoice-details mt-4">
                <div class="w-50 me-4">
                    <div class="form-group">
                        <asp:Label ID="lblCliente" runat="server" Text="Cliente:"></asp:Label>
                        <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCliente_TextChanged"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblDireccion" runat="server" Text="Dirección:"></asp:Label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="w-50">
                    <div class="form-group">
                        <asp:Label ID="lblDocumento" runat="server" Text="Documento:"></asp:Label>
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:"></asp:Label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="product-section">
                <h4>Agregar Artículos</h4>
                <div class="d-flex align-items-end mb-3">
                    <div class="form-group me-2 flex-grow-1">
                        <asp:Label ID="lblArticulo" runat="server" Text="Artículo (ID o Nombre):"></asp:Label>
                        <asp:TextBox ID="txtArticulo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group me-2">
                        <asp:Label ID="lblCantidad" runat="server" Text="Cantidad:"></asp:Label>
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number" Text="1"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnAgregarArticulo" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregarArticulo_Click" />
                </div>
                <asp:Label ID="lblArticuloMensaje" runat="server" ForeColor="Red"></asp:Label>
                <asp:GridView ID="gvFacturaDetalles" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-3" OnRowDeleting="gvFacturaDetalles_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="ProductoID" HeaderText="ID" />
                        <asp:BoundField DataField="NombreProducto" HeaderText="Descripción" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
                        <asp:CommandField ShowDeleteButton="True" DeleteText="Quitar" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="totals-section text-end">
                <div class="form-group">
                    <asp:Label ID="lblTotalBruto" runat="server" Text="Subtotal: "></asp:Label>
                    <asp:TextBox ID="txtTotalParcial" runat="server" ReadOnly="true" CssClass="form-control d-inline-block w-25"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblIva" runat="server" Text="IVA (13%): "></asp:Label>
                    <asp:TextBox ID="txtIVA" runat="server" ReadOnly="true" CssClass="form-control d-inline-block w-25"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblTotal" runat="server" Text="Total a Pagar: "></asp:Label>
                    <asp:TextBox ID="txtTotal" runat="server" ReadOnly="true" CssClass="form-control d-inline-block w-25"></asp:TextBox>
                </div>
            </div>

            <div class="d-flex justify-content-end mt-4">
                <asp:Button ID="btnGuardarFactura" runat="server" Text="Guardar Factura" CssClass="btn btn-primary me-2" OnClick="btnGuardarFactura_Click" />
                <asp:Button ID="btnVolver1" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
            </div>

        </asp:Panel>

        <asp:Panel ID="pnlVerFactura" runat="server" Visible="false" CssClass="p-4 border rounded shadow-sm bg-light">
            <h2>Buscar y Ver Factura</h2>
            <hr />
            <div class="form-group d-flex align-items-center">
                <asp:Label ID="lblNumeroFactura" runat="server" Text="Número de Factura:" CssClass="me-2"></asp:Label>
                <asp:TextBox ID="txtNumeroFactura" runat="server" CssClass="form-control me-2"></asp:TextBox>
                <asp:Button ID="btnBuscarFactura" runat="server" Text="Buscar" CssClass="btn btn-primary me-2" OnClick="btnBuscarFactura_Click" />
                <asp:Button ID="btnVolver2" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
            </div>
            <asp:Label ID="lblBusquedaMensaje" runat="server" ForeColor="Red" CssClass="mt-2"></asp:Label>
            
            <asp:Panel ID="pnlFacturaEncontrada" runat="server" Visible="false" CssClass="mt-4">
                <h3>Detalles de la Factura #<asp:Label ID="lblFacturaEncontradaNum" runat="server"></asp:Label></h3>
                <div class="client-details-section">
                    <div class="form-group">
                        <asp:Label ID="lblClienteEncontrado" runat="server" Text="Cliente: "></asp:Label>
                        <asp:Label ID="lblClienteNombre" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblDireccionEncontrada" runat="server" Text="Dirección: "></asp:Label>
                        <asp:Label ID="lblDireccionFactura" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblTelefonoEncontrado" runat="server" Text="Teléfono: "></asp:Label>
                        <asp:Label ID="lblTelefonoFactura" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCorreoEncontrado" runat="server" Text="Email: "></asp:Label>
                        <asp:Label ID="lblCorreo" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblFechaEncontrada" runat="server" Text="Fecha: "></asp:Label>
                        <asp:Label ID="lblFecha" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
                <asp:GridView ID="gvFacturaEncontradaDetalles" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:BoundField DataField="ProductoID" HeaderText="ID Artículo" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>

    </div>

</asp:Content>