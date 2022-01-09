using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeskShare.Helper
{
    public class ApiConnector
    {
        public HttpClient _Client { get; private set; }

        public ApiConnector(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            var ba = new Uri("http://api");
            httpClient.BaseAddress = ba;


            var jwt = httpContextAccessor.HttpContext.Request.Cookies["Tkn"];


            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "de,en-US;q=0.7,en;q=0.3");
            httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:65.0) Gecko/20100101 Firefox/65.0");
            httpClient.DefaultRequestHeaders.Add("Accept",
                "text/plain,application/json,text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            httpClient.Timeout = TimeSpan.FromMinutes(10);


            _Client = httpClient;

        }
    }
}
