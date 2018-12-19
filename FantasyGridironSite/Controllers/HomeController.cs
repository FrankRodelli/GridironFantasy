using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FantasyGridironSite.Models;
using FantasyGridironSite.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;

namespace FantasyGridironSite.Controllers
{
    public class HomeController : Controller
    {
        FantasyAPI _api = new FantasyAPI();

        public async Task<IActionResult> Index()
        {
            List<Player> players = new List<Player>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/players");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                players = JsonConvert.DeserializeObject<List<Player>>(result);
            }

            return View(players);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            List<Player> players = new List<Player>();
            Player player = new Player();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/players");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                players = JsonConvert.DeserializeObject<List<Player>>(result);
                player = players.Where(s => s.Id == Id).FirstOrDefault();
            }

            return View(player);
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(Player player)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.PostAsJsonAsync("api/players/" + player.Id, JsonConvert.SerializeObject(player));

            Console.Write(res.Content);
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
