// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginLogic.cs" company="">
//   
// </copyright>
// <summary>
//   Handles everything related to logging in
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SE2StackOverflow
{
    using System.Collections.Specialized;

    using Oracle.DataAccess.Client;

    using SE2StackOverflow.Logic;

    /// <summary>
    /// Handles everything related to logging in
    /// </summary>
    public class LoginLogic
    {
        /// <summary>
        /// Tries to log the user in
        /// </summary>
        /// <param name="input">
        /// Form with username and password
        /// </param>
        /// <returns>
        /// User ID. Null if no user found or unable to log in
        /// </returns>
        public static int? Login(NameValueCollection input)
        {
            input = Validator.ValidateForm(input);

            Database db = DatabaseSingleton.GetInstance();

            string username = input["username"];

            string query = string.Format("SELECT password, ident FROM USERS where username = '{0}'", username);

            OracleDataReader reader = db.QueryDb(query);
            if (reader == null || !reader.HasRows)
            {
                return null;
            }

            reader.Read();

            if (input["password"] == reader[0].ToString())
            {
                // Password matches
                return int.Parse(reader[1].ToString());
            }

            // Password does not match
            return null;
        }
    }
}