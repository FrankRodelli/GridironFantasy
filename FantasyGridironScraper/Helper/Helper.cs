using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace FantasyGridironSite.Helper
{
    public class FantasyAPI
    {

        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://fantasygridiron.azurewebsites.net/");
            return Client;
        }
    }
}
