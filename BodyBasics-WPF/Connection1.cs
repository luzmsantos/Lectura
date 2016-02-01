using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    class Connection1
    {
        //public MySqlConnection conect = new MySqlConnection("persistsecurityinfo=True; server=localhost; user= root; password=12345; database=bodies");
        public MySqlConnection conect = new MySqlConnection("server=localhost; user= root; password=12345; database=body-db");

        public void connect()
        {
            try
            {
                conect.Open();
            }
            catch (MySqlException ex)
            {
                String error = ex.ToString();
            }
        }

        public void desconectar()
        {
            conect.Close();
        }
    }
}