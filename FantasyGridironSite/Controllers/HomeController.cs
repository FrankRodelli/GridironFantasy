using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Microsoft.AspNetCore.Mvc;
using FantasyGridironSite.Models;
using FantasyGridironSite.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FantasyGridironSite.Controllers
{
    public class HomeController : Controller
    {
        FantasyAPI _api = new FantasyAPI();
        IEnumerable<Player> playerList;

        public async Task<IActionResult> Index(string sortOrder, string currentFilter,string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "PlayerName" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<Player> players = new List<Player>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/players");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                playerList = JsonConvert.DeserializeObject<List<Player>>(result);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                playerList = playerList.Where(s => s.PlayerName.ToLower().Contains(searchString.ToLower())
                    || s.PlayerCollege.ToLower().Contains(searchString.ToLower()) || s.PlayerTeam.ToLower().Contains(searchString.ToLower()));
            }

            return View(playerList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Player player)
        {
            ModelState.AddModelError(string.Empty, "Is it working?");

            using (var client = _api.Initial())
            {

                JObject jo = JObject.Parse(JsonConvert.SerializeObject(player));
                jo.Property("Id").Remove();

                //HTTP POST
                var postTask = client.PostAsJsonAsync("api/players", jo);
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.  " + jo);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.  " + result.Content.ReadAsStringAsync().Result);

            }


            return View(player);


        }

        public async Task<ActionResult> Delete(string Id)
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

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteConfirmed(string Id)
        {
            using (var client = _api.Initial())
            {
                var postTask = client.DeleteAsync("api/players/" + Id );
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error " + result.Content.ReadAsStringAsync().Result);
                }
            }

                return View();
        }

        public IActionResult Edit(string Id)
        {
            using (var client = _api.Initial())
            {
                var postTask = client.GetAsync("api/players/" + Id);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Player player = JsonConvert.DeserializeObject<Player>(result.Content.ReadAsStringAsync().Result);
                    return View(player);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            using (var client = _api.Initial())
            {
                JObject jo = JObject.Parse(JsonConvert.SerializeObject(player));

                var postTask = client.PutAsJsonAsync("api/players/" + player.Id, jo);
                postTask.Wait();

                var result = postTask.Result;
            }

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
