using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Route66_SKP_SKAL_Assignment.Scripts.Helpers
{
    public class SqlHelper
    {
        readonly bool _isKraken = true; //Set this to false to make the code use Miniks connection string
        private string _connStringName = "KrakenConnString";
        private const string ConnStringName2 = "MinikConnString";

        public DataRow[] GetDataFromDatabase(string query) //Returns rows in table
        {
            if(!_isKraken)
            {
                _connStringName = ConnStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[_connStringName].ConnectionString))
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
            if (!_isKraken)
            {
                _connStringName = ConnStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[_connStringName].ConnectionString))
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
            if (!_isKraken)
            {
                _connStringName = ConnStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[_connStringName].ConnectionString))
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
            if (!_isKraken)
            {
                _connStringName = ConnStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[_connStringName].ConnectionString))
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
            var result = false;

            if (!_isKraken)
            {
                _connStringName = ConnStringName2;
            }

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings[_connStringName].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn)
                {
                    CommandTimeout = 5
                };
                var rows = (int)cmd.ExecuteScalar();

                if (rows > 0)
                {
                    result = true;
                }

                return result;
            }
        }
    }
}