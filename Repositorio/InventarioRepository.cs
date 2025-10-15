using Proyecto_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Repositorio
{
    public class InventarioRepository
    {
        Conexion conexion = new Conexion();
        public List<Productos> CargarProductos()
        {
            List<Productos> lista = new List<Productos>();
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();

                    string query = "SELECT ProductoID, NombreProducto, Descripcion, Precio, Stock, Categoria FROM Productos ORDER BY ProductoID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Productos producto = new Productos
                            {
                                ProductoID = reader.GetInt32(0),
                                NombreProducto = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Precio = reader.GetDecimal(3),
                                Stock = reader.GetInt32(4),
                                Categoria = reader.GetString(5)
                            };

                            lista.Add(producto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Podés registrar el error o lanzar una excepción si querés manejarlo afuera
                Console.WriteLine("Error al cargar productos: " + ex.Message);
            }

            return lista;
        }

        public void InsertarProducto(Productos producto)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = @"INSERT INTO Productos (NombreProducto, Descripcion, Precio, Stock, Categoria) 
                 VALUES (@NombreProducto, @Descripcion, @Precio, @Stock, @Categoria)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@Categoria", producto.Categoria);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar producto: " + ex.Message);
            }
        }

        public void ActualizarProducto(Productos producto)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = @"UPDATE Productos 
                    SET NombreProducto = @NombreProducto,
                     Descripcion = @Descripcion,
                     Precio = @Precio,
                     Stock = @Stock,
                     Categoria = @Categoria
                    WHERE ProductoID = @ProductoID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@Categoria", producto.Categoria);
                        cmd.Parameters.AddWithValue("@ProductoID", producto.ProductoID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar producto: " + ex.Message);
            }
        }

        public void EliminarProducto(int id)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = @"Delete from Productos Where ProductoID = @ProductoID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al elimninar producto: " + ex.Message);
            }
        }
    }
}