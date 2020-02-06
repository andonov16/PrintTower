using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Print_Tower
{
    static class DBConnect
    {
        private static MySqlConnection connection;
        public static string server { get; private set; }
        public static string database { get; private set; }
        public static string uid { get; private set; }
        public static string password { get; private set; }

        public static bool Connected { get; private set; }
        public static string TableName { get; set; }

        static DBConnect()
        {
            if (Directory.Exists("data"))
                if (File.Exists("data/DBinfo.txt"))
                {
                    string[] info = File.ReadAllLines("data/DBinfo.txt");
                    if(info.Length == 8)
                    {
                        server = info[0];
                        database = info[1];
                        uid = info[2];
                        password = info[3];
                        TableName = info[4];
                        string connectionS = "SERVER=" + server + ";" + "DATABASE=" +
                        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                        connection = new MySqlConnection(connectionS);

                        try
                        {
                            MainWindow.dt.Interval = new TimeSpan(int.Parse(info[5]), int.Parse(info[6]), int.Parse(info[7]));
                            return;
                        }
                        catch (Exception)
                        {
                            MainWindow.dt.Interval = new TimeSpan(1,0,0);
                            return;
                        }
                    }
                }
                else
                    File.Create("data/DBinfo.txt").Close();
            else
            {
                Directory.CreateDirectory("data");
                File.Create("data/DBinfo.txt").Close();
            }

            server = "copycontrol.dyndns.org";
            database = "Printtower";
            uid = "miro";
            password = "miro";
            TableName = "Devstore";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        public static void SaveChanges(string newIP, string newDBname, string newUser, string newPass, 
                                       string newTableName, int hours, int minutes, int seconds)
        {
            server = newIP;
            database = newDBname;
            uid = newUser;
            password = newPass;
            TableName = newTableName;
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            MainWindow.dt.Interval = new TimeSpan(hours, minutes, seconds);

            File.WriteAllLines(@"data/DBinfo.txt",new string[]{ server, database, uid, password, TableName,
                                                                hours.ToString(), minutes.ToString(), seconds.ToString()});
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