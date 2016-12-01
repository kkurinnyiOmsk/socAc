using RestSharp;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// return info for next actions
    /// </summary>
    public class LoginInfo
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        /// <summary>
        /// csrftoken и т.д.
        /// </summary>
        public IEnumerable<RestResponseCookie> ReturnedCookies { get; set; }
    }
}