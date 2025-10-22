using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using global::Proyecto_progra1_v1.Modelos.Entidades;
using Proyecto_progra1_v1.Pages.Facturas;


namespace Proyecto_progra1_v1.Modelos.Repositorios
{
        public class FacturaRepository
        {
            private ConexionDB _conexion = new ConexionDB(); // Asumo que ConexionDB está accesible

        /// <summary>
        /// Guarda la factura y todos sus detalles en una sola transacción.
        /// </summary>
        /// <param name="clienteID">ID del cliente asociado a la factura.</param>
        /// <param name="totalFactura">El total a pagar de la factura (Subtotal + IVA).</param>
        /// <param name="items">Lista de artículos de la factura.</param>
        /// <returns>El ID de la factura insertada.</returns>
        /// 
        public Factura ObtenerFacturaPorId(int facturaID)
        {
            Factura factura = null;

            // Asumo que tienes una entidad Factura.cs con SubtotalFactura, IVAFactura y Total.
            // Si no tienes Factura.cs, crea una clase llamada Factura para mapear los datos principales.

            using (SqlConnection con = _conexion.Conectar())
            {
                con.Open();

                // 1. Obtener la cabecera (incluyendo Subtotal e IVA)
                string queryFactura = @"
            SELECT 
                FacturaID, ClienteID, Fecha, 
                SubtotalFactura, IVAFactura, Total 
            FROM Facturas 
            WHERE FacturaID = @FacturaID";

                using (SqlCommand cmdFactura = new SqlCommand(queryFactura, con))
                {
                    cmdFactura.Parameters.AddWithValue("@FacturaID", facturaID);
                    SqlDataReader reader = cmdFactura.ExecuteReader();

                    if (reader.Read())
                    {
                        factura = new Factura
                        {
                            FacturaID = reader.GetInt32(0),
                            ClienteID = reader.GetInt32(1),
                            Fecha = reader.GetDateTime(2),

                            SubtotalFactura = reader.GetDecimal(3),
                            IVAFactura = reader.GetDecimal(4),
                            Total = reader.GetDecimal(5),
                            Items = new List<FacturaItem>() // Inicializar la lista de ítems
                        };
                    }
                    reader.Close();
                }

                // 2. Obtener los detalles (ítems)
                if (factura != null)
                {
                    string queryDetalle = @"
                SELECT 
                    ProductoID, Cantidad, PrecioUnitario, 
                    (SELECT NombreProducto FROM Productos WHERE ProductoID = DetalleFactura.ProductoID) AS NombreProducto 
                FROM DetalleFactura 
                WHERE FacturaID = @FacturaID";

                    using (SqlCommand cmdDetalle = new SqlCommand(queryDetalle, con))
                    {
                        cmdDetalle.Parameters.AddWithValue("@FacturaID", facturaID);
                        SqlDataReader readerDetalle = cmdDetalle.ExecuteReader();

                        while (readerDetalle.Read())
                        {
                            int cantidad = readerDetalle.GetInt32(1);
                            decimal precio = readerDetalle.GetDecimal(2);

                            factura.Items.Add(new FacturaItem
                            {
                                ProductoID = readerDetalle.GetInt32(0),
                                Cantidad = cantidad,
                                Precio = precio,
                                NombreProducto = readerDetalle["NombreProducto"].ToString(),
                                Total = cantidad * precio // Calcular el total del ítem al cargar
                            });
                        }
                        readerDetalle.Close();
                    }
                }
            }
            return factura;
        }
        public int GuardarFactura(int clienteID, decimal subtotalFactura, decimal ivaFactura, decimal totalFactura, List<FacturaItem> items)
        {
                int facturaID = -1;

                using (SqlConnection con = _conexion.Conectar())
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        // 1. Insertar en la tabla Facturas
                        // La columna Total en Facturas NO permite NULL, por lo que debe ser provista.
                        string insertFacturaQuery = "INSERT INTO Facturas (ClienteID, Fecha, SubtotalFactura, IVAFactura, Total) VALUES (@ClienteID, @Fecha, @Subtotal, @IVA, @Total); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdFactura = new SqlCommand(insertFacturaQuery, con, transaction);
                        cmdFactura.Parameters.AddWithValue("@ClienteID", clienteID);
                        cmdFactura.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        cmdFactura.Parameters.AddWithValue("@Subtotal", subtotalFactura); // Debes pasar este valor
                        cmdFactura.Parameters.AddWithValue("@IVA", ivaFactura);
                        cmdFactura.Parameters.AddWithValue("@Total", totalFactura);

                        facturaID = Convert.ToInt32(cmdFactura.ExecuteScalar());

                        // 2. Insertar en la tabla DetalleFactura
                        // CORRECCIÓN CLAVE: Se elimina 'Subtotal' porque es una columna calculada en la DB.
                        string insertDetalleQuery = "INSERT INTO DetalleFactura (FacturaID, ProductoID, Cantidad, PrecioUnitario) VALUES (@FacturaID, @ProductoID, @Cantidad, @PrecioUnitario)";

                        foreach (var item in items)
                        {
                            SqlCommand cmdDetalle = new SqlCommand(insertDetalleQuery, con, transaction);
                            cmdDetalle.Parameters.AddWithValue("@FacturaID", facturaID);
                            cmdDetalle.Parameters.AddWithValue("@ProductoID", item.ProductoID);
                            cmdDetalle.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                            cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", item.Precio);

                            // NOTA: EL PARÁMETRO @Subtotal FUE ELIMINADO PARA EVITAR EL ERROR DE COLUMNA CALCULADA

                            cmdDetalle.ExecuteNonQuery();
                        }

                        // 3. Confirmar la transacción
                        transaction.Commit();
                        return facturaID;
                    }
                    catch (Exception ex)
                    {
                        // Si algo falla, deshace todos los cambios y relanza el error.
                        transaction.Rollback();
                        throw new Exception("Error al procesar la factura en la base de datos: " + ex.Message, ex);
                    }
                }
            }
        }
    }
