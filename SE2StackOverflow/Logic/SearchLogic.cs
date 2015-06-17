// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchLogic.cs" company="">
//   
// </copyright>
// <summary>
//   Provides logic for searching
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SE2StackOverflow.Logic
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    ///     Provides logic for searching
    /// </summary>
    public class SearchLogic
    {
        /// <summary>
        /// Static function to search
        /// </summary>
        /// <param name="input">
        /// Form that needs to be processed
        /// </param>
        /// <returns>
        /// A JSON list that has all related questions
        /// </returns>
        public static List<Dictionary<string, string>> Search(NameValueCollection input)
        {
            input = Validator.ValidateForm(input);

            var userQuery = input["query"];

            var query = string.Format("select * from post where title LIKE '%{0}%' OR postbody LIKE '%{0}%'", userQuery);

            return DatabaseSingleton.GetInstance().GetJsonQuery(query);
        }
    }
}