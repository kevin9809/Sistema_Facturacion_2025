using Proyecto_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Repositorio
{
    public class ClientesRepository
    {
        Conexion conexion = new Conexion();
        public List<Clientes> CargarClientes()
        {
            List<Clientes> lista = new List<Clientes>();
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();

                    string query = "SELECT ID_Cliente, Nombre, Direccion, Telefono, Email, ID_Usuario FROM Clientes";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clientes cliente = new Clientes
                            {
                                ID_Cliente = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Direccion = reader.GetString(2),
                                Telefono = reader.GetString(3),
                                Email = reader.GetString(4),
                                //ID_Usuario = reader.GetInt32(5)
                            };

                            lista.Add(cliente);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Podés registrar el error o lanzar una excepción si querés manejarlo afuera
                Console.WriteLine("Error al cargar clientes: " + ex.Message);
            }

            return lista;
        }

        public void InsertarCliente(Clientes cliente)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "INSERT INTO Clientes (Nombre, Direccion, Telefono, Email, ID_Usuario) " +
                                   "VALUES (@Nombre, @Direccion, @Telefono, @Email, @ID_Usuario)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.Parameters.AddWithValue("@ID_Usuario", cliente.ID_Usuario);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar cliente: " + ex.Message);
            }
        }

        public void EditarCliente(Clientes cliente)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "UPDATE Clientes " +
                    "SET Nombre = @Nombre, " +
                    "Direccion = @Direccion, " +
                    "Telefono = @Telefono, " +
                    "Email = @Email, " +
                    "ID_Usuario = @ID_Usuario " +
                    "WHERE ID_Cliente = @ID_Cliente";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID_Cliente", cliente.ID_Cliente);
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.Parameters.AddWithValue("@ID_Usuario", cliente.ID_Usuario);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar cliente: " + ex.Message);
            }
        }

        public void EliminarCliente(int cliente)
        {
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    string query = "Delete from Clientes where ID_Cliente = @ID_Cliente";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID_Cliente", cliente);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar cliente: " + ex.Message);
            }
        }

        public Clientes ObtenerClientePorNombre(string nombreCliente)
        {
            Clientes cliente = null;
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();
                    // Usamos LIKE para buscar nombres parciales o exactos
                    string query = "SELECT ID_Cliente, Nombre, Direccion, Telefono, Email, ID_Usuario FROM Clientes WHERE Nombre LIKE @NombreCliente";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Agregamos comodines '%' para buscar coincidencias parciales
                        cmd.Parameters.AddWithValue("@NombreCliente", "%" + nombreCliente + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cliente = new Clientes
                                {
                                    ID_Cliente = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Direccion = reader.GetString(2),
                                    Telefono = reader.GetString(3),
                                    Email = reader.GetString(4),
                                    // Aseguramos que la columna no sea NULL antes de leerla
                                    ID_Usuario = reader.IsDBNull(5) ? 0 : reader.GetInt32(5)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // En un proyecto real, se debe registrar la excepción (log)
                // Por ahora, lanzamos para depuración:
                throw new Exception("Error al obtener cliente por nombre: " + ex.Message);
            }
            return cliente;
        }
    }
}