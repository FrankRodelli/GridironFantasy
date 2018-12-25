using System;
using System.Collections.Generic;
using FantasyGridironSite.Models;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Web;
//Your get player data method was working but when  the methods are used together there's an out of index exception and it freezes
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

                for (int i = 0; i < 25; i++)
                {
                    try
                    {
                        //Awful workaround for scraping player ID's
                        var playerLink = (div.ChildNodes[2/*Selects table container*/]
                            .ChildNodes[1/*Selects Inner tbody*/]
                            .ChildNodes[2/*Selects each row in table*/]
                            .ChildNodes[i/*Selects row(player)*/]
                            .ChildNodes[1/*Selects name area container*/]
                            .ChildNodes[0/*Selects name area*/]
                            .ChildNodes[1/*Selects name span*/]
                            .ChildNodes[3/*selects inner name span*/]
                            .ChildNodes[0/*Selects player link*/]
                            .Attributes[1/*gets link from href*/]
                            .DeEntitizeValue);

                        int pos = playerLink.LastIndexOf("/") + 1;
                        playerID.Add(playerLink.Substring(pos, playerLink.Length - pos));
                    }
                    catch
                    {

                    }


                }
            }
        }

        public void GetPlayerData()
        {
            foreach(var id in playerID)
            {
                try
                {
                    Player player = new Player();
                    var url = "https://sports.yahoo.com/nfl/players/" + id + "/";
                    var web = new HtmlWeb();
                    var doc = web.Load(url);

                    var pid = doc.DocumentNode.SelectNodes("//*[@class='ys-name']");
                    player.YHId = pid[0].InnerHtml;

                    players.Add(player);
                }
                catch
                {

                }

            }
            
        }

        public List<Player> GetSomething()
        {
            return players;
        }
    }
}
