using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE2StackOverflow
{
    using System.Data;
    using System.IO;
    using System.Windows.Forms;

    using Oracle.DataAccess.Client;

    public class Database
    {
        private OracleConnection oracleConnection;

        /// <summary>
        ///     Connects to our database
        /// </summary>
        public Database()
        {
            // make sure the dbmanager can't reinitialise
            if (oracleConnection != null
                && (oracleConnection.State != ConnectionState.Broken || oracleConnection.State != ConnectionState.Closed))
            {
                return;
            }

            oracleConnection = new OracleConnection();
            oracleConnection.ConnectionString = string.Format("User Id={0};Password={1};Data Source=//{2}:{3}/{4}",
                "toets", "test", "localhost", "1521", "xe");
            try
            {
                oracleConnection.Open();
            }
            catch (OracleException)
            {
                Environment.Exit(1);
            }
        }

        /// <summary>
        ///     Sends a query to the database and returns the result
        /// </summary>
        /// <param name="query">Query for the database</param>
        /// <returns>Object containing the result</returns>
        public OracleDataReader QueryDB(string query)
        {
            TimeSpan time = DateTime.MaxValue - DateTime.MinValue;
            OracleDataReader queryResult;

            OracleCommand oracleCommand = oracleConnection.CreateCommand();

            // Replace ; with EOS, so that this won't ever be a problem again
            oracleCommand.CommandText = query.Replace(';', '\0');

            // Prepare for docking
            oracleCommand.Prepare();

            try
            {
                //Logger.Info("Querying Database");
                //Logger.Debug("Query: " + query);
                queryResult = oracleCommand.ExecuteReader();
                //Aangezien OracleDB zich niet aan de ACID standaard houd, committen wij na iedere query.
                // Waarom ondersteunen ze niet gewoon ACID?
                oracleCommand.CommandText = "commit";
                oracleCommand.ExecuteReader();
                //Logger.Success("Query Successfully executed");
            }
            catch (OracleException exception)
            {
                //Logger.Error("Error querying datbase: " + exception.ToString());
                Console.WriteLine(exception.ToString());

                return null;
            }

            return queryResult;
        }

        public List<Dictionary<string, string>> GetJSONQuery(string query)
        {
            List<Dictionary<string, string>> ret = new List<Dictionary<string, string>>();
            OracleDataReader reader = this.QueryDB(query);

            var columns = new List<string>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i).ToLower());
            }

            while (reader.Read())
            {
                Dictionary<string, string> d = new Dictionary<string, string>();

                foreach (var column in columns)
                {
                    d.Add(column, reader[column].ToString());
                }

                ret.Add(d);
            }

            return ret;
        }
    }

    public static class DatabaseSingleton
    {
        private static Database db = null;

        public static Database GetInstance()
        {
            if (db == null)
            {
                db = new Database();
            }

            return db;
        }
    }
}