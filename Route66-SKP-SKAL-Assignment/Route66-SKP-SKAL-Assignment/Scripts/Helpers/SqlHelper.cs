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
        readonly bool isKraken = true; //Set this to false to make the code use Minik's connection string
        string connStringName = "KrakenConnString";
        readonly string connStringName2 = "MinikConnString";

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

        public DataSet GetSetFromDatabase(string query) //Returns DataSet
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
                da.Fill(ds);

                return ds;
            }
        }

        public void SetDataToDatabase(string query) //Adds new data
        {
            if (!isKraken)
            {
                connStringName = connStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn)
                {
                    CommandTimeout = 5
                };
                cmd.ExecuteReader();
            }
        }

        //public DataTable GetSPDataFromDatabase(string query) //Returns Data from an SP
        //{
        //    if (!isKraken)
        //    {
        //        connStringName = connStringName2;
        //    }

        //    using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
        //    {
        //        conn.Open();

        //        var cmd = new MySqlCommand(query, conn)
        //        {
        //            CommandType = CommandType.StoredProcedure,
        //            CommandTimeout = 5
        //        };

        //        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
        //        {
        //            DataTable dt = new DataTable();
        //            sda.Fill(dt);

        //            return dt;
        //        }
        //    }
        //}

        public int UpdateDataToDatabase(string query) //Returns rows updated
        {
            if (!isKraken)
            {
                connStringName = connStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn)
                {
                    CommandTimeout = 5
                };
                var rowsUpdated = cmd.ExecuteNonQuery();

                return rowsUpdated;
            }
        }

        public bool CheckDataFromDatabase(string query) //Returns true if row exists
        {
            bool result = false;

            if (!isKraken)
            {
                connStringName = connStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn)
                {
                    CommandTimeout = 5
                };
                int rows = (int)cmd.ExecuteScalar();

                if (rows > 0)
                {
                    result = true;
                }

                return result;
            }
        }
    }
}