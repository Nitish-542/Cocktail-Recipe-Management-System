using CRM.Interfaces;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CocktailController : ControllerBase
    {
        private readonly ICocktailService _cocktailService;

        // Constructor injection of ICocktailService
        public CocktailController(ICocktailService cocktailService)
        {
            _cocktailService = cocktailService;
        }

        // CREATE: api/cocktail
        [HttpPost("create")]
        public async Task<ActionResult<CocktailDTO>> CreateCocktail([FromBody] CocktailDTO cocktailDto)
        {
            //  Debugging Log
            Console.WriteLine("🔹 Received POST request to create cocktail.");

            if (cocktailDto == null)
            {
                Console.WriteLine("❌ Received NULL cocktailDto.");
                return BadRequest("Invalid request. Data is missing.");
            }

            // Log received values
            Console.WriteLine($"📌 Data received: DrinkName={cocktailDto.DrinkName}, BartenderId={cocktailDto.BartenderId}, CategoryId={cocktailDto.CategoryId}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState is INVALID.");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
                return BadRequest(ModelState);
            }

            var createdCocktailDto = await _cocktailService.CreateCocktailAsync(cocktailDto);

            if (createdCocktailDto == null)
            {
                Console.WriteLine("⚠️ Failed to create cocktail.");
                return StatusCode(500, "Internal Server Error: Cocktail creation failed.");
            }

            Console.WriteLine($"✅ Cocktail Created: ID={createdCocktailDto.DrinkId}");
            return CreatedAtAction(nameof(GetCocktail), new { id = createdCocktailDto.DrinkId }, createdCocktailDto);
        }


        // READ: api/cocktail/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CocktailDTO>> GetCocktail(int id)
        {
            var cocktail = await _cocktailService.GetCocktailByIdAsync(id);
            if (cocktail == null)
            {
                return NotFound();
            }
            return cocktail;
        }

        // READ: api/cocktail
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CocktailDTO>>> GetCocktails()
        {
            var cocktails = await _cocktailService.GetAllCocktailsAsync();
            return Ok(cocktails);
        }

        // UPDATE: api/cocktail/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCocktail(int id, CocktailDTO cocktailDto)
        {
            var success = await _cocktailService.UpdateCocktailAsync(id, cocktailDto);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/cocktail/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCocktail(int id)
        {
            var success = await _cocktailService.DeleteCocktailAsync(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
