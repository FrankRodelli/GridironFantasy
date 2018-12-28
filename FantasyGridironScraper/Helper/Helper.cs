using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace FantasyGridironScraper.Helper
{
    public class FantasyGridironScraper
    {

        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://fantasygridiron.azurewebsites.net/");
            return Client;
        }
    }
}
