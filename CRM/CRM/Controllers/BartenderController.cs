using CRM.Interfaces;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BartenderController : ControllerBase
    {
        private readonly IBartenderService _bartenderService;

        public BartenderController(IBartenderService bartenderService)
        {
            _bartenderService = bartenderService;
        }

        // GET: api/bartender
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BartenderDto>>> GetBartenders()
        {
            var bartenders = await _bartenderService.GetAllBartendersAsync();
            return Ok(bartenders);
        }

        // GET: api/bartender/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BartenderDto>> GetBartender(int id)
        {
            var bartender = await _bartenderService.GetBartenderByIdAsync(id);
            if (bartender == null)
            {
                return NotFound();
            }
            return Ok(bartender);
        }

        // POST: api/bartender
        [HttpPost]
        public async Task<ActionResult<BartenderDto>> CreateBartender(BartenderDto bartenderDto)
        {
            var createdBartenderDto = await _bartenderService.CreateBartenderAsync(bartenderDto);
            return CreatedAtAction(nameof(GetBartender), new { id = createdBartenderDto.BartenderId }, createdBartenderDto);
        }

        // PUT: api/bartender/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBartender(int id, BartenderDto bartenderDto)
        {
            if (id != bartenderDto.BartenderId)
            {
                return BadRequest();
            }

            var success = await _bartenderService.UpdateBartenderAsync(id, bartenderDto);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        // DELETE: api/bartender/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBartender(int id)
        {
            var success = await _bartenderService.DeleteBartenderAsync(id);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
