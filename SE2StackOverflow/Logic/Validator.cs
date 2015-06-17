// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Validator.cs" company="">
//   
// </copyright>
// <summary>
//   Handles validations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SE2StackOverflow.Logic
{
    using System.Collections.Specialized;

    /// <summary>
    /// Handles validations
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Validates an entire form
        /// </summary>
        /// <param name="form">
        /// Form that was filled in by a user
        /// </param>
        /// <returns>
        /// Clean form
        /// </returns>
        public static NameValueCollection ValidateForm(NameValueCollection form)
        {
            NameValueCollection ret = new NameValueCollection();
            foreach (string key in form.Keys)
            {
                ret[key] = form[key];
                if (!key.StartsWith("_"))
                {
                    // Escape single quote characters
                    ret.Set(key, ret[key].Replace("'", "''"));

                    // Escape backslashes
                    ret.Set(key, ret[key].Replace("\\", "\\\\"));

                    // Remove semi-colons
                    ret.Set(key, ret[key].Replace(";", string.Empty));
                }
            }

            return ret;
        }
    }
}