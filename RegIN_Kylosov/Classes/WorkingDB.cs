﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using MySql.Data.MySqlClient;

namespace RegIN_Kylosov.Classes
{
    public class WorkingDB
    {
        readonly static string connection = "server=192.168.0.111;port=3306;database=regin;user=root;pwd=;";

        public static MySqlConnection OpenConnection()
        {
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(connection);
                mySqlConnection.Open();
                return mySqlConnection;
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
                return null;
            }
        }


        public static MySqlDataReader Query(string Sql, MySqlConnection mySqlConnection)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(Sql, mySqlConnection);
            return mySqlCommand.ExecuteReader();
        }

        public static void CloseConnection(MySqlConnection mySqlConnection)
        {
            mySqlConnection.Close();
            MySqlConnection.ClearPool(mySqlConnection);
        }

        public static bool OpenConnection(MySqlConnection mySqlConnection)
        {
            return mySqlConnection != null && mySqlConnection.State == System.Data.ConnectionState.Open;
        }
    }
}
