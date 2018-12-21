using System.Collections.Generic;
using FantasyGridiron.Models;
using FantasyGridiron.Services;
using Microsoft.AspNetCore.Mvc;

namespace FantasyGridiron.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayersController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public ActionResult<List<Player>> Get()
        {
            return _playerService.Get();
        }

        [HttpGet("{id}", Name = "GetPlayer")]
        public ActionResult<Player> Get(string id)
        {
            var player = _playerService.Get(id);

            if(player == null)
            {
                return NotFound();
            }

            return player;
        }

        [HttpPost]
        public ActionResult<Player> Create(Player player)
        {
            _playerService.Create(player);

            return CreatedAtRoute("GetPlayer", new { id = player.Id.ToString() }, player);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Player playerIn)
        {
            var player = _playerService.Get(id);

            if(player == null)
            {
                return NotFound();
            }

            _playerService.Update(id, playerIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var player = _playerService.Get(id);

            if(player == null)
            {
                return NotFound();
            }

            _playerService.Remove(player.Id);

            return NoContent();
        }
    }
}
