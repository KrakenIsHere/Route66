using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Route66_SKP_SKAL_Assignment.Scripts.Helpers
{
    public class SqlHelper
    {
        bool isKraken = true; //Set this to false to make the code use Minik's connection string
        string connStringName = "KrakenConnString";
        string connStringName2 = "MinikConnString";

        public DataRow[] GetDataFromDatabase(string query) //Returns rows in table
        {
            if(!isKraken)
            {
                connStringName = connStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                var rows = dt.AsEnumerable().ToArray();

                return rows;
            }
        }

        public DataSet GetTableFromDatabase(string query) //Returns rows in table
        {
            if (!isKraken)
            {
                connStringName = connStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
            {
                conn.Open();

                var da = new MySqlDataAdapter(query, conn);
                var ds = new DataSet();
                da.Fill(ds, "EmployeeDetails");

                return ds;
            }
        }

        public void SetDataToDatabase(string query) //Adds new data
        {
            if (!isKraken)
            {
                connStringName = connStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                cmd.ExecuteReader();
            }
        }

        public int UpdateDataToDatabase(string query) //Returns rows updated
        {
            if (!isKraken)
            {
                connStringName = connStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                var rowsUpdated = cmd.ExecuteNonQuery();

                return rowsUpdated;
            }
        }
    }
}