using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE2StackOverflow.Logic
{
    using System.Collections.Specialized;
    using System.Configuration;

    public class Validator
    {
        public static NameValueCollection ValidateForm(NameValueCollection form)
        {
            NameValueCollection ret = new NameValueCollection();
            foreach (string key in form.Keys)
            {
                ret[key] = form[key];
                if (!key.StartsWith("_"))
                {
                    //Escape single quote characters
                    ret.Set(key, ret[key].Replace("'", "''"));
                    // Escape backslashes
                    ret.Set(key, ret[key].Replace("\\", "\\\\"));
                    // Remove semi-colons
                    ret.Set(key, ret[key].Replace(";", ""));
                }
            }

            return ret;
        }
    }
}