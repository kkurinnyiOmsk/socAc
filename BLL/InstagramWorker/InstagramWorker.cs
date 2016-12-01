using NLog;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace BLL
{
    public class InstagramWorker : IInstagramWorker
    {
        private static NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        private string USER_AGENT;

        private RestClient client;

        public InstagramWorker()
        {
            USER_AGENT =
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_3) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/45.0.2414.0 Safari/537.36";

            client = new RestClient("https://www.instagram.com/");
            client.CookieContainer = new CookieContainer();
            client.UserAgent = USER_AGENT;


            //var proxy = new WebProxy("адрес прокси");
            //proxy.Credentials = new NetworkCredential("логин", "пароль");
            //client.Proxy = proxy;

        }

        public bool Follow(LoginInfo loginInfo, long targetUser)
        {
            var followReq = new RestRequest(string.Format("/web/friendships/{0}/follow/", targetUser), Method.POST);
            foreach (var c in loginInfo.ReturnedCookies)
            {
                if (c.Name == "csrftoken")
                {
                    followReq.AddHeader("X-CSRFToken", c.Value);
                }
                else
                    followReq.AddCookie(c.Name, c.Value);
            }
            followReq.AddHeader("X-Requested-With", "XMLHttpRequest");
            followReq.AddHeader("X-Instagram-AJAX", "1");
            followReq.AddHeader("Referer", client.BaseUrl.ToString());
            followReq.AddHeader("origin", "https://www.instagram.com");
            followReq.AddHeader("content-type", "application/x-www-form-urlencoded");


            var followResponse = client.Execute(followReq);
            return followResponse.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public LoginInfo Login(string login, string password)
        {
            var firstRequest = new RestRequest("/", Method.GET);
            var firstResponse = client.Execute(firstRequest);
            var csrftoken = firstResponse.Cookies.First(x => x.Name == "csrftoken").Value;

            var loginRequest = new RestRequest("/accounts/login/ajax/", Method.POST);
            loginRequest.AddHeader("X-CSRFToken", csrftoken);
            loginRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            loginRequest.AddHeader("X-Instagram-AJAX", "1");
            loginRequest.AddHeader("Referer", client.BaseUrl.ToString());
            loginRequest.AddParameter("username", login);
            loginRequest.AddParameter("password", password);

            var loginResponse = client.Execute(loginRequest);


            return new LoginInfo
            {
                IsSuccess = loginResponse.StatusCode == HttpStatusCode.OK ? true : false, 
                ReturnedCookies = loginResponse.Cookies
            };
        }
    }
}
