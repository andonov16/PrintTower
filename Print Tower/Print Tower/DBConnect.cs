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
            uid = "miro";
            password = "miro";
            TableName = "Devstore";
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

        public static void Insert(string devName, string devIP, bool devStatus,
                                  string Prop1Name, string Prop1Val, string Prop2Name, string Prop2Val,
                                  string Prop3Name, string Prop3Val, DateTime LastCheck)
        {
            string query = @"INSERT INTO " + TableName + " " +
                "(DeviceName,IPAddress,Status,Prop1Name,Prop1Value,Prop2Name,Prop2Value,Prop3Name,Prop3Value,LastCheck)" +
                " VALUES(" + '"' + devName + '"' + "," + '"' + devIP + '"' + "," +
                '"' + devStatus + '"' + "," + '"' + Prop1Name + '"' + "," +
                '"' + Prop1Val + '"' + "," + '"' + Prop2Name + '"' + "," +
                '"' + Prop2Val + '"' + "," + '"' + Prop3Name + '"' + "," +
                '"' + Prop3Val + '"' + "," + '"' + LastCheck + '"' + ")";
            Console.WriteLine(query);
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
    }
}