using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Print_Tower
{
    static class DBConnect
    {
        private static MySqlConnection connection;
        public static string server;
        public static string database;
        public static string uid;
        public static string password;

        public static bool Connected { get; private set; }
        public static string TableName { get; set; }

        static DBConnect()
        {
            server = "5.53.211.84";
            database = "Printtower";
            uid = "root";
            password = "dojcho";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        public static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return Connected = true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
        public static bool CloseConnection()
        {
            connection.Close();
            return true;
        }

        public static void Insert(string devName)
        {
            TableName = "Devstore";
            string query = @"INSERT INTO " + TableName + " (DeviceName) VALUES('" + devName + "')";

            if (DBConnect.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                DBConnect.CloseConnection();
            }
        }
        public static void Delete(string columnName, string columnVal)
        {
            string query = "DELETE FROM " + TableName + " WHERE " + columnName + "='" + columnVal + "'";

            if (DBConnect.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                DBConnect.CloseConnection();
            }
        }
        public static void Update(string devName, string columnName, string columnVal)
        {
            //"UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'"
            string query = "UPDATE " + TableName + " SET " + columnName + "=" + columnVal + " WHERE DevName='" + devName + "'";

            //Open connection
            if (DBConnect.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                DBConnect.CloseConnection();
            }
        }
    }
}
