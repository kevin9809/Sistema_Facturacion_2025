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
        public Productos ObtenerProductoPorNomOId(string articulo)
        {
            Productos producto = null;
            int id;

            // Intenta determinar si el input es un ID numérico
            bool esId = int.TryParse(articulo, out id);

            try
            {
                // Asumiendo que '_conexion' es una instancia de tu clase Conexion
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "";

                    // Importante: La tabla en la DB es 'Inventario', y las columnas deben coincidir
                    string campos = "ProductoID, NombreProducto, Descripcion, Precio, Stock, Categoria";

                    if (esId)
                    {
                        // Busca por el ID exacto
                        query = $"SELECT {campos} FROM dbo.Productos WHERE ProductoID = @Articulo AND Stock > 0";
                    }
                    else
                    {
                        // Busca por nombre (LIKE) y asegura que haya stock disponible
                        query = $"SELECT {campos} FROM dbo.Productos WHERE NombreProducto LIKE @Articulo AND Stock > 0";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);

                    if (esId)
                    {
                        cmd.Parameters.AddWithValue("@Articulo", id);
                    }
                    else
                    {
                       
                        cmd.Parameters.AddWithValue("@Articulo", "%" + articulo + "%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new Productos 
                            {
                                ProductoID = Convert.ToInt32(reader["ProductoID"]),
                                NombreProducto = reader["NombreProducto"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Stock = Convert.ToInt32(reader["Stock"]), 
                                Categoria = reader["Categoria"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Siempre es bueno lanzar una excepción para que el controlador pueda manejar el error
                throw new Exception("Error al obtener producto por nombre o ID en el repositorio: " + ex.Message);
            }

            return producto;
        }
    }
}