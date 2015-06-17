﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="">
//   
// </copyright>
// <summary>
//   The sql operator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SE2StackOverflow
{
    using System;
    using System.Collections.Generic;

    using Oracle.DataAccess.Client;

    /// <summary>
    ///     The sql operator.
    /// </summary>
    public enum SqlOperator
    {
        /// <summary>
        ///     The min.
        /// </summary>
        Min, 

        /// <summary>
        ///     The max.
        /// </summary>
        Max
    }

    /// <summary>
    ///     Handles the connection and queries to the database
    /// </summary>
    public class Database
    {
        /// <summary>
        ///     The oracle connection.
        /// </summary>
        private readonly OracleConnection oracleConnection;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Database" /> class.
        ///     Connects to our database
        /// </summary>
        public Database()
        {
            // Set up the connection string
            this.oracleConnection = new OracleConnection();
            this.oracleConnection.ConnectionString = string.Format(
                "User Id={0};Password={1};Data Source=//{2}:{3}/{4}", 
                "toets", 
                "test", 
                "localhost", 
                "1521", 
                "xe");

            this.oracleConnection.Open();
        }

        /// <summary>
        /// Sends a query to the database and returns the result
        /// </summary>
        /// <param name="query">
        /// Query for the database
        /// </param>
        /// <returns>
        /// Object containing the result
        /// </returns>
        public OracleDataReader QueryDb(string query)
        {
            OracleDataReader queryResult;

            var oracleCommand = this.oracleConnection.CreateCommand();

            // Replace ; with EOS, so that this won't ever be a problem again
            oracleCommand.CommandText = query.Replace(';', '\0');

            // Prepare for docking
            oracleCommand.Prepare();

            try
            {
                queryResult = oracleCommand.ExecuteReader();

                // Aangezien OracleDB zich niet aan de ACID standaard houd, committen wij na iedere query.
                // Waarom ondersteunen ze niet gewoon ACID?
                oracleCommand.CommandText = "commit";
                oracleCommand.ExecuteReader();
            }
            catch (OracleException exception)
            {
                Console.WriteLine(exception.ToString());

                return null;
            }

            return queryResult;
        }

        /// <summary>
        /// Executes a query and turns it into a JSON-lite
        /// </summary>
        /// <param name="query">
        /// Query to be executed
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Dictionary<string, string>> GetJsonQuery(string query)
        {
            var ret = new List<Dictionary<string, string>>();
            var reader = this.QueryDb(query);

            // We start reading the columns
            var columns = new List<string>();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i).ToLower());
            }

            // Now we read the reader
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

        /// <summary>
        /// Executes an operation that returns a single value
        /// </summary>
        /// <param name="tableName">
        /// Name of the table to be queried
        /// </param>
        /// <param name="oper">
        /// Operator to be used
        /// </param>
        /// <returns>
        /// Result of the query
        /// </returns>
        public string SingleIdentOperation(string tableName, SqlOperator oper)
        {
            var reader = this.QueryDb(string.Format("SELECT {0}(ident) FROM {1}", oper, tableName));

            try
            {
                reader.Read();
                return reader[0].ToString();
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    ///     Singleton for the Database class
    /// </summary>
    public static class DatabaseSingleton
    {
        /// <summary>
        ///     The db.
        /// </summary>
        private static Database db;

        /// <summary>
        ///     Returns a single instance of the database
        /// </summary>
        /// <returns>Database connection</returns>
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