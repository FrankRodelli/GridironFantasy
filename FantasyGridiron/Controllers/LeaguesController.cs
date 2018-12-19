using System.Collections.Generic;
using FantasyGridiron.Models;
using FantasyGridiron.Services;
using Microsoft.AspNetCore.Mvc;

namespace FantasyGridiron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly LeagueService _leagueService;

        public LeaguesController(LeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        [HttpGet]
        public ActionResult<List<League>> Get()
        {
            return _leagueService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetLeague")]
        public ActionResult<League> Get(string id)
        {
            var league = _leagueService.Get(id);

            if (league == null)
            {
                return NotFound();
            }

            return league;
        }

        [HttpPost]
        public ActionResult<League> Create(League league)
        {
            _leagueService.Create(league);

            return CreatedAtRoute("GetLeague", new { id = league.Id.ToString() }, league);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, League leagueIn)
        {
            var league = _leagueService.Get(id);

            if (league == null)
            {
                return NotFound();
            }

            _leagueService.Update(id, leagueIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var league = _leagueService.Get(id);

            if (league == null)
            {
                return NotFound();
            }

            _leagueService.Remove(league.Id);

            return NoContent();
        }
    }
}
