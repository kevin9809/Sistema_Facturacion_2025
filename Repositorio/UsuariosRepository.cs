using Proyecto_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Repositorio
{
    public class UsuariosRepository
    {
        Conexion conexion = new Conexion();
        public List<Usuarios> CargarUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                using (SqlConnection con = conexion.Conectar())
                {
                    con.Open();

                    string query = "Select * from Usuarios";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuarios usuario = new Usuarios
                            {
                                ID_Usuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Contraseña = reader.GetString(2),
                                Email = reader.GetString(3),
                                Rol = reader.GetString(4)
                            };

                            lista.Add(usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Podés registrar el error o lanzar una excepción si querés manejarlo afuera
                Console.WriteLine("Error al cargar usuarios: " + ex.Message);
            }

            return lista;
        }
    }
}