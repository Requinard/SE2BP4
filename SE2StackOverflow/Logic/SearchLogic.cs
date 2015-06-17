using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE2StackOverflow.Logic
{
    using System.Collections.Specialized;

    public class SearchLogic
    {
        public static List<Dictionary<string, string>> Search(NameValueCollection input)
        {
            input = Validator.ValidateForm(input);

            string user_query = input["query"];

            string query = string.Format(
                "select * from post where title LIKE '%{0}%' OR postbody LIKE '%{0}%'",
                user_query);

            return DatabaseSingleton.GetInstance().GetJSONQuery(query);
        }
    }
}