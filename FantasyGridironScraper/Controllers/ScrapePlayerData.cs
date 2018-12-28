using System;
using System.Collections.Generic;
using FantasyGridironSite.Models;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace FantasyGridironScraper
{
    public class ScrapePlayerData
    {
        private List<string> playerID = new List<string>();
        private List<Player> players = new List<Player>();

        public ScrapePlayerData()
        {
            GetPlayerIDs();
            GetPlayerData();
        }



        public void GetPlayerIDs()
        {
            for (int c = 0; c < 1080; c = c + 25)
            {
                //Sets URL for current team in list and navigates to page
                var url = "https://football.fantasysports.yahoo.com/f1/701815/players?status=ALL&pos=O&cut_type=9&stat1=S_S_2018&myteam=0&sort=AR&sdir=1&count=" + c;
                var web = new HtmlWeb();
                var doc = web.Load(url);

                var div = doc.GetElementbyId("players-table");

                int rows = 25;
                for (int i = 0; i < rows; i++)
                {

                    //Awful workaround for scraping player ID's
                    var playerLink = div.ChildNodes[2/*Selects table container*/]
                        .ChildNodes[1/*Selects Inner tbody*/]
                        .ChildNodes[2/*Selects each row in table*/]
                        .ChildNodes[i/*Selects row(player)*/]
                        .ChildNodes[1/*Selects name area container*/]
                        .ChildNodes[0/*Selects name area*/]
                        .ChildNodes[1/*Selects name span*/]
                        .ChildNodes[3/*selects inner name span*/]
                        .ChildNodes[0/*Selects player link*/]
                        .Attributes[1/*gets link from href*/]
                        .DeEntitizeValue;

                    int pos = playerLink.LastIndexOf("/") + 1;


                    playerID.Add(playerLink.Substring(pos, playerLink.Length - pos));

                    rows = (div.ChildNodes[2/*Selects table container*/]
                        .ChildNodes[1/*Selects Inner tbody*/]
                        .ChildNodes[2/*Selects each row in table*/].ChildNodes).Count;

                }
            }

            
        }

        public void GetPlayerData()
        {
            Parallel.ForEach(playerID, (id) =>
            {
                try
                {
                    Player player = new Player();
                    var url = "https://sports.yahoo.com/nfl/players/" + id;
                    var web = new HtmlWeb();
                    var doc = web.Load(url);

                    //Set YHID
                    player.YHId = id; //id

                    //Set player data
                    var pid = doc.DocumentNode.SelectNodes("//*[@class='ys-name']");
                    player.PlayerName = pid[0].InnerHtml;

                    pid = doc.DocumentNode.SelectNodes("//*[@class='Row Mb(15px) Fz(14px)']");
                    player.PlayerNumber = Regex.Replace(pid[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerHtml, @"[^\d]", "");

                    player.PlayerPosition = Regex.Replace(pid[0].ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText, @"[,]", "");

                    player.PlayerTeam = Regex.Replace(pid[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].InnerText, @"[,]", "");

                    pid = doc.DocumentNode.SelectNodes("//*[@class='Fz(13px) Lh(1.8) Maw(600px)']");

                    player.PlayerHeight = HtmlEntity.DeEntitize(pid[0].ChildNodes[0].ChildNodes[1].InnerText);

                    player.PlayerWeight = HtmlEntity.DeEntitize(pid[0].ChildNodes[1].ChildNodes[1].InnerText);

                    //player.PlayerDrafted = (HtmlEntity.DeEntitize(pid[0].ChildNodes[5].ChildNodes[1].InnerText)).Substring(0, 4);

                    player.PlayerCollege = HtmlEntity.DeEntitize(pid[0].ChildNodes[3].ChildNodes[1].InnerText);

                    pid = doc.DocumentNode.SelectNodes("//*[@class='IbBox Bgr(nr) Pos(r) Miw(228px) W(228px) H(228px) Bgp(50%,25px) Bgz(135%) Bdrs(50%) Mend(40px)']");

                    AddPlayer(player);
                }
                catch { 
}


            });

        }

        public void AddPlayer(Player player)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://fantasygridiron.azurewebsites.net/");


            JObject jo = JObject.Parse(JsonConvert.SerializeObject(player));
            jo.Property("Id").Remove();

            //HTTP POST
            var postTask = client.PostAsJsonAsync("api/players", jo);
            postTask.Wait();


        }

        public List<Player> GetSomething()
        {
            return players;
        }
    }
}
