using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using global::Proyecto_progra1_v1.Modelos.Entidades;



    namespace Proyecto_progra1_v1.Modelos.Repositorios
    {
        public class ClienteRepository
        {
            // Instancia de tu clase de conexión
            private ConexionDB _conexion = new ConexionDB();

            public Cliente ObtenerClientePorNombre(string nombreCliente)
            {
                Cliente cliente = null;

                try
                {
                    using (SqlConnection con = _conexion.Conectar())
                    {
                        con.Open();
                        // Consulta adaptada para tu tabla Clientes (Direccion, Telefono, Email)
                        string query = "SELECT [ID_Cliente], [Direccion], [Telefono], [Email], [ID_Usuario] FROM Clientes WHERE Nombre = @Nombre";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", nombreCliente);
                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                cliente = new Cliente
                                {
                                    ID_Cliente = reader.GetInt32(0),
                                    Nombre = nombreCliente,
                                    Direccion = reader["Direccion"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    ID_Usuario = reader.GetInt32(4) 
                                };
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception)
                {
                    // Si hay un error de DB, devolvemos null y el controlador lo maneja.
                }
                return cliente;
            }

            public int ObtenerIdCliente(string nombreCliente)
            {
                // Reutiliza la función de arriba para obtener solo el ID de forma limpia.
                Cliente cliente = ObtenerClientePorNombre(nombreCliente);
                return cliente != null ? cliente.ID_Cliente : -1;
            }
        /// <summary>
        /// Obtiene los datos completos de un cliente usando su ID.
        /// </summary>
        /// <param name="clienteID">El ID del cliente.</param>
        /// <returns>Objeto Cliente si se encuentra, o null.</returns>
        public Cliente ObtenerClientePorId(int clienteID)
        {
            Cliente cliente = null;

            try
            {
                using (SqlConnection con = _conexion.Conectar())
                {
                    con.Open();
                    // Consulta para buscar al cliente por su ID_Cliente
                    string query = "SELECT Nombre, Direccion, Telefono, Email, ID_Usuario FROM Clientes WHERE ID_Cliente = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", clienteID);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                ID_Cliente = clienteID, // Ya conocemos el ID
                                Nombre = reader["Nombre"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Email = reader["Email"].ToString(),
                                ID_Usuario = reader.GetInt32(4) // El índice 4 corresponde a ID_Usuario
                            };
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception)
            {
                // En un entorno real, podrías registrar el error aquí.
            }
            return cliente;
        }
    }
    }

