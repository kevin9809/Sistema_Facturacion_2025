using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_MVC.Repositorio
{
    public class Conexion
    {
        private string connectionString;
        public Conexion()
        {
            connectionString = ConfigurationManager.ConnectionStrings["InventarioDB"].ConnectionString;
        }
        public SqlConnection Conectar()
        {
            return new SqlConnection(connectionString);
        }
    }
}