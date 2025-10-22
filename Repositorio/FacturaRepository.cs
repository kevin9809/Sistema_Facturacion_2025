using Proyecto_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Proyecto_MVC.Repositorio
{
    public class FacturaRepository
    {
        private Conexion _conexion = new Conexion();
        private const decimal IVA_RATE = 0.13m;
        public int GuardarFactura(Facturas factura)
        {
            SqlTransaction transaction = null;
            SqlConnection con = null;

            try
            {
                con = _conexion.Conectar();
                con.Open();

                transaction = con.BeginTransaction();

                // INSERCIÓN EN CABECERA (Facturas)
                string queryCabecera = @"INSERT INTO Facturas (ClienteID, Fecha, Total) 
                                             VALUES (@ClienteID, @Fecha, @Total); 
                                             SELECT SCOPE_IDENTITY();";

                SqlCommand cmdCabecera = new SqlCommand(queryCabecera, con, transaction);
                cmdCabecera.Parameters.AddWithValue("@ClienteID", factura.ClienteID);
                cmdCabecera.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmdCabecera.Parameters.AddWithValue("@Total", factura.Total);

                int idFactura = Convert.ToInt32(cmdCabecera.ExecuteScalar());

                // INSERCIÓN EN DETALLE (DetalleFactura)
                // Nota: El Subtotal en DetalleFactura es una columna CALCULADA por la DB.
                string queryDetalle = @"INSERT INTO DetalleFactura (FacturaID, ProductoID, PrecioUnitario, Cantidad)
                                             VALUES (@FacturaID, @ProductoID, @PrecioUnitario, @Cantidad)";

                foreach (var item in factura.Items)
                {
                    SqlCommand cmdDetalle = new SqlCommand(queryDetalle, con, transaction);
                    cmdDetalle.Parameters.AddWithValue("@FacturaID", idFactura); 
                    cmdDetalle.Parameters.AddWithValue("@ProductoID", item.ProductoID);
                    cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", item.Precio);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", item.Cantidad);

                    cmdDetalle.ExecuteNonQuery();
                }

                transaction.Commit();
                return idFactura;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rbEx)
                    {
                        throw new Exception("Error al intentar deshacer la transacción: " + rbEx.Message);
                    }
                }
                throw new Exception("Error al guardar la factura en el repositorio: " + ex.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        // --------------------------------------------------
        // MÉTODO: ObtenerTodasLasFacturas (para listado y búsqueda)
        // Modificado para recalcular Subtotal e IVA para la vista de listado.
        // --------------------------------------------------
        public IEnumerable<Facturas> ObtenerTodasLasFacturas(string search = "")
        {
            List<Facturas> lista = new List<Facturas>();

            string query = @"
                SELECT 
                    F.FacturaID, F.Fecha, F.Total, 
                    C.Nombre AS NombreCliente 
                FROM 
                    Facturas F
                INNER JOIN 
                    Clientes C ON F.ClienteID = C.ID_Cliente 
                WHERE
                    (@Search IS NULL OR @Search = '' 
                    OR CAST(F.FacturaID AS VARCHAR) LIKE '%' + @Search + '%' 
                    OR C.Nombre LIKE '%' + @Search + '%')
                ORDER BY 
                    F.FacturaID DESC";

            using (SqlConnection con = _conexion.Conectar())
            {
                SqlCommand cmd = new SqlCommand(query, con);

                // Parámetro para la búsqueda (maneja nulo o vacío)
                cmd.Parameters.AddWithValue("@Search", (object)search ?? DBNull.Value);

                try
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal totalLeido = Convert.ToDecimal(reader["Total"]);
                            // Subtotal = Total / (1 + IVA_RATE)
                            decimal subtotalRecalculado = totalLeido / (1 + IVA_RATE);
                            // IVA = Total - Subtotal
                            decimal ivaRecalculado = totalLeido - subtotalRecalculado;


                            lista.Add(new Facturas
                            {
                                FacturaID = Convert.ToInt32(reader["FacturaID"]),
                                NombreCliente = reader["NombreCliente"].ToString(),
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                Total = totalLeido, // Usamos el total de la DB

                                // ASIGNAMOS LOS VALORES RECALCULADOS
                                SubtotalFactura = subtotalRecalculado,
                                IVAFactura = ivaRecalculado
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener la lista de facturas: " + ex.Message, ex);
                }
            }
            return lista;
        }

        // --------------------------------------------------
        // MÉTODO: ObtenerFacturaPorId (Se mantiene el cálculo para el detalle)
        // --------------------------------------------------
        public Facturas ObtenerFacturaPorId(int id)
        {
            Facturas factura = null;
            // ClientesRepository clienteRepo = new ClientesRepository(); // Esta línea es redundante aquí

            try
            {
                using (SqlConnection con = _conexion.Conectar())
                {
                    con.Open();
                    // 1. Consultar la CABECERA de la Factura y el Cliente
                    string queryCabecera = @"
                        SELECT F.FacturaID, F.ClienteID, F.Fecha, F.Total, 
                               C.Nombre, C.Direccion, C.Email, C.Telefono
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

                                // El Total SÍ se lee de la DB
                                Total = Convert.ToDecimal(readerCabecera["Total"]),

                                NombreCliente = readerCabecera["Nombre"].ToString(),
                                DireccionCliente = readerCabecera["Direccion"].ToString(),
                                EmailCliente = readerCabecera["Email"].ToString(),

                                Items = new List<FacturaItem>()
                            };
                        }
                    }

                    if (factura != null)
                    {
                        // 2. Consultar el DETALLE de la Factura (Items)
                        string queryDetalle = @"
                            SELECT DF.ProductoID, P.NombreProducto as NombreProducto, 
                                   DF.PrecioUnitario as Precio, DF.Cantidad, 
                                   DF.Subtotal as Total 
                            FROM DetalleFactura DF
                            INNER JOIN Productos P ON DF.ProductoID = P.ProductoID
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
                                    NombreProducto = readerDetalle["NombreProducto"].ToString(),
                                    Precio = Convert.ToDecimal(readerDetalle["Precio"]),
                                    Cantidad = Convert.ToInt32(readerDetalle["Cantidad"]),
                                    Total = Convert.ToDecimal(readerDetalle["Total"])
                                });
                            }
                        }
                        decimal subtotalRecalculado = factura.Items.Sum(i => i.Total);
                        decimal ivaRecalculado = subtotalRecalculado * IVA_RATE;

                        factura.SubtotalFactura = subtotalRecalculado;
                        factura.IVAFactura = ivaRecalculado;

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
