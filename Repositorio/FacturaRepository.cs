using Proyecto_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Proyecto_MVC.Repositorio;

namespace Proyecto_MVC.Repositorio
{
    public class FacturaRepository
    {
        private Conexion _conexion = new Conexion();

        public bool GuardarFactura(Facturas factura)
        {
            try
            {
                using (SqlConnection con = _conexion.Conectar())
                {
                    con.Open();

                    // INSERCIÓN EN CABECERA (Facturas)
                    // La cabecera está correcta, SubtotalFactura es calculado y se omite.
                    string queryCabecera = @"INSERT INTO Facturas (ClienteID, Fecha, IVAFactura, Total) 
                                             VALUES (@ClienteID, @Fecha, @IVAFactura, @Total);
                                             SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdCabecera = new SqlCommand(queryCabecera, con);
                    cmdCabecera.Parameters.AddWithValue("@ClienteID", factura.ClienteID);
                    cmdCabecera.Parameters.AddWithValue("@Fecha", factura.Fecha);
                    cmdCabecera.Parameters.AddWithValue("@IVAFactura", factura.IVAFactura);
                    cmdCabecera.Parameters.AddWithValue("@Total", factura.Total);

                    // Obtener el nuevo ID de la factura
                    int idFactura = Convert.ToInt32(cmdCabecera.ExecuteScalar());

                    string queryDetalle = @"INSERT INTO DetalleFactura (FacturaID, ProductoID, PrecioUnitario, Cantidad)
                                             VALUES (@FacturaID, @ProductoID, @Precio, @Cantidad)";

                    foreach (var item in factura.Items)
                    {
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, con);
                        cmdDetalle.Parameters.AddWithValue("@FacturaID", idFactura);
                        cmdDetalle.Parameters.AddWithValue("@ProductoID", item.ProductoID);

                        // NOTA: @Precio en C# se mapea a PrecioUnitario en la DB.
                        cmdDetalle.Parameters.AddWithValue("@Precio", item.Precio);

                        cmdDetalle.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        // cmdDetalle.Parameters.AddWithValue("@NombreProducto", item.NombreProducto); // ¡ELIMINADO!
                        // cmdDetalle.Parameters.AddWithValue("@Total", item.Total); // ¡ELIMINADO!

                        cmdDetalle.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                // Ahora, este error debería resolverse. El error de columna era la causa raíz.
                throw new Exception("Error al guardar la factura en el repositorio: " + ex.Message);
            }
        }


        public Facturas ObtenerFacturaPorId(int id)
        {
            Facturas factura = null;

            // Necesitas una referencia a ClienteRepository para obtener el nombre
            // Se asume que ClientesRepository está en el mismo namespace o tiene un using.
            ClientesRepository clienteRepo = new ClientesRepository();

            try
            {
                using (SqlConnection con = _conexion.Conectar())
                {
                    con.Open();

                    // 1. Consultar la CABECERA de la Factura y el Cliente
                    // (Asumo que has añadido 'Telefono' al SELECT y a las propiedades de Facturas.cs)
                    string queryCabecera = @"
                        SELECT F.*, C.Nombre, C.Direccion, C.Email, C.Telefono 
                        FROM Facturas F
                        INNER JOIN Clientes C ON F.ClienteID = C.ID_Cliente 
                        WHERE F.FacturaID = @FacturaID";

                    SqlCommand cmdCabecera = new SqlCommand(queryCabecera, con);
                    cmdCabecera.Parameters.AddWithValue("@FacturaID", id);

                    using (SqlDataReader readerCabecera = cmdCabecera.ExecuteReader())
                    {
                        if (readerCabecera.Read())
                        {
                            factura = new Facturas
                            {
                                FacturaID = Convert.ToInt32(readerCabecera["FacturaID"]),
                                ClienteID = Convert.ToInt32(readerCabecera["ClienteID"]),
                                Fecha = Convert.ToDateTime(readerCabecera["Fecha"]),
                                // NOTA: 'SubtotalFactura' en la DB probablemente es calculado (y debería ser leído)
                                SubtotalFactura = Convert.ToDecimal(readerCabecera["SubtotalFactura"]),
                                IVAFactura = Convert.ToDecimal(readerCabecera["IVAFactura"]),
                                Total = Convert.ToDecimal(readerCabecera["Total"]),

                                // Datos del Cliente (Unidos en la consulta)
                                NombreCliente = readerCabecera["Nombre"].ToString(),
                                DireccionCliente = readerCabecera["Direccion"].ToString(),
                                EmailCliente = readerCabecera["Email"].ToString(),

                                Items = new List<FacturaItem>() // Inicializar la lista de detalles
                            };
                        }
                    } // Fin readerCabecera

                    if (factura != null)
                    {
                        // 2. Consultar el DETALLE de la Factura (Items)
                        // *** CORRECCIÓN: Se agrega JOIN con Productos para obtener NombreProducto ***
                        // Se corrigen los nombres de columna para coincidir con el esquema
                        string queryDetalle = @"
                            SELECT DF.ProductoID, P.Nombre as NombreProducto, 
                                   DF.PrecioUnitario as Precio, DF.Cantidad, 
                                   DF.Subtotal as Total 
                            FROM DetalleFactura DF
                            INNER JOIN Productos P ON DF.ProductoID = P.ProductoID -- Se asume P.ProductoID es el PK de Productos
                            WHERE DF.FacturaID = @FacturaID";

                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, con);
                        cmdDetalle.Parameters.AddWithValue("@FacturaID", id);

                        using (SqlDataReader readerDetalle = cmdDetalle.ExecuteReader())
                        {
                            while (readerDetalle.Read())
                            {
                                factura.Items.Add(new FacturaItem
                                {
                                    ProductoID = Convert.ToInt32(readerDetalle["ProductoID"]),
                                    // Se lee como alias 'NombreProducto'
                                    NombreProducto = readerDetalle["NombreProducto"].ToString(),
                                    // Se lee como alias 'Precio'
                                    Precio = Convert.ToDecimal(readerDetalle["Precio"]),
                                    Cantidad = Convert.ToInt32(readerDetalle["Cantidad"]),
                                    // Se lee como alias 'Total'
                                    Total = Convert.ToDecimal(readerDetalle["Total"])
                                });
                            }
                        } // Fin readerDetalle
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la factura del repositorio: " + ex.Message);
            }

            return factura;
        }
    }
}