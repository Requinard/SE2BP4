namespace SE2StackOverflow
{
    using System.Collections.Specialized;
    using System.Web;

    using SE2StackOverflow.Logic;

    public class LoginLogic
    {
        public static int? Login(NameValueCollection input)
        {
            input = Validator.ValidateForm(input);

            var db = DatabaseSingleton.GetInstance();

            var username = input["username"];

            var query = string.Format("SELECT password, ident FROM USERS where username = '{0}'", username);

            var reader = db.QueryDB(query);
            if (reader == null || !reader.HasRows) return null;
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