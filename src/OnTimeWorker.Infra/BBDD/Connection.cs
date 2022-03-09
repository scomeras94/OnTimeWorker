using MySqlConnector;
using System;

namespace OnTimeWorker.Infra.BBDD
{
    public class Connection
    {
        private static string _cn = "SERVER=127.0.0.1;PORT=3306;DATABASE=businessmanagement;UID=admin;PASSWORD=admin;";
        private static MySqlConnection _conn = Setup();

        private static MySqlConnection Setup()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(_cn);
                return conn;
            }
            catch (Exception) {
                //return new Error() { code = 1, program = "Connection.cs", text = "Setup", message = "Cannont asign connection" };
                return null;
            }
        }

        public static MySqlConnection GetConnection()
        {
            try
            {
                return _conn;
            }
            catch (Exception) { return null; }
        }

        public static void OpenConnection()
        {
            try
            {
                _conn.Open();
            }
            catch (Exception) { }
        }

        public static void CloseConnection()
        {
            try
            {
                _conn.Close();
            }
            catch (Exception) { }
        }
    }
}
