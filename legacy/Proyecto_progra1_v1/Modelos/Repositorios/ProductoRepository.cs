using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Proyecto_progra1_v1.Modelos.Entidades;

namespace Proyecto_progra1_v1.Modelos.Repositorios
{
    public class ProductoRepository
    {
        private ConexionDB _conexion = new ConexionDB();

        public Producto ObtenerProductoPorIdONombre(string busqueda)
        {
            Producto producto = null;

            // Asume que la búsqueda puede ser el ID (numérico) o el nombre (texto)
            bool esId = int.TryParse(busqueda, out int productoId);

            try
            {
                using (SqlConnection con = _conexion.Conectar())
                {
                    con.Open();
                    string query = "";

                    if (esId)
                    {
                        query = "SELECT ProductoID, NombreProducto, Descripcion, Precio, Stock, Categoria FROM Productos WHERE ProductoID = @Busqueda";
                    }
                    else
                    {
                        query = "SELECT ProductoID, NombreProducto, Descripcion, Precio, Stock, Categoria FROM Productos WHERE NombreProducto = @Busqueda";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Busqueda", esId ? (object)productoId : busqueda);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            producto = new Producto
                            {
                                ProductoID = reader.GetInt32(0),
                                NombreProducto = reader["NombreProducto"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Precio = reader.GetDecimal(3),
                                Stock = reader.GetInt32(4),
                                Categoria = reader["Categoria"].ToString()
                            };
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception)
            {
                // Manejo de errores
            }
            return producto;
        }
    }
}
