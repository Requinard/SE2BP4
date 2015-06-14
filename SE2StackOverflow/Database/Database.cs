namespace SE2StackOverflow
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Oracle.DataAccess.Client;

    public enum SQLOperator
    {
        MIN,

        MAX
    }

    public class Database
    {
        private readonly OracleConnection oracleConnection;

        /// <summary>
        ///     Connects to our database
        /// </summary>
        public Database()
        {
            // make sure the dbmanager can't reinitialise
            if (this.oracleConnection != null
                && (this.oracleConnection.State != ConnectionState.Broken
                    || this.oracleConnection.State != ConnectionState.Closed))
            {
                return;
            }

            this.oracleConnection = new OracleConnection();
            this.oracleConnection.ConnectionString = string.Format(
                "User Id={0};Password={1};Data Source=//{2}:{3}/{4}",
                "toets",
                "test",
                "localhost",
                "1521",
                "xe");
            try
            {
                this.oracleConnection.Open();
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
            var time = DateTime.MaxValue - DateTime.MinValue;
            OracleDataReader queryResult;

            var oracleCommand = this.oracleConnection.CreateCommand();

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
            var ret = new List<Dictionary<string, string>>();
            var reader = this.QueryDB(query);

            var columns = new List<string>();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i).ToLower());
            }

            while (reader.Read())
            {
                var d = new Dictionary<string, string>();

                foreach (var column in columns)
                {
                    d.Add(column, reader[column].ToString());
                }

                ret.Add(d);
            }

            return ret;
        }

        public string SingleIdentOperation(string tableName, SQLOperator oper)
        {
            OracleDataReader reader = QueryDB(String.Format("SELECT {0}(ident) FROM {1}", oper, tableName));

            reader.Read();

            return reader[0].ToString();
        }
    }

    public static class DatabaseSingleton
    {
        private static Database db;

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