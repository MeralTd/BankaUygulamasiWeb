using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Banka_uyg
{
    public class GlobalVariables
    {
        public static HttpClient WEbApiClient = new HttpClient();

        static GlobalVariables()
        {
            WEbApiClient.BaseAddress = new Uri("http://localhost:62084/api/");
            WEbApiClient.DefaultRequestHeaders.Clear();
            WEbApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
        }
    }
}