using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_progra1_v1
{
    [RunInstaller(true)]
    public partial class ConexionDB : System.Configuration.Install.Installer
    {
        private string connectionString;
        public ConexionDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["InventarioDB"].ConnectionString;
        }
        public SqlConnection Conectar()
        {
            return new SqlConnection(connectionString);
        }
    }
}
