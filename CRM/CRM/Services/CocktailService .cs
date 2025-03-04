using CRM.Data;
using CRM.Interfaces;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class CocktailService : ICocktailService
    {
        private readonly ApplicationDbContext _context;

        public CocktailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CocktailDTO>> GetAllCocktailsAsync()
        {
            return await _context.Cocktails
                .Include(c => c.Category)
                .Include(c => c.Bartender)
                .Select(c => new CocktailDTO
                {
                    DrinkId = c.DrinkId,
                    BartenderId = c.BartenderId,
                    DrinkName = c.DrinkName,
                    DrinkRecipe = c.DrinkRecipe,
                    LiqIns = c.LiqIns,
                    MixIns = c.MixIns,
                    CategoryName = c.Category == null ? null : c.Category.CategoryName,
                    DatePosted = c.DatePosted
                })
                .ToListAsync();
        }

        public async Task<CocktailDTO> GetCocktailByIdAsync(int id)
        {
            var cocktail = await _context.Cocktails
                .Include(c => c.Bartender)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.DrinkId == id);

            if (cocktail == null)
            {
                return null;
            }

            var cocktailDto = new CocktailDTO
            {
                DrinkId = cocktail.DrinkId,
                DrinkName = cocktail.DrinkName,
                DrinkRecipe = cocktail.DrinkRecipe,
                LiqIns = cocktail.LiqIns,
                MixIns = cocktail.MixIns,
                CategoryId = cocktail.CategoryId,
                Category = new CategoryDTO
                {
                    CategoryId = cocktail.Category.CategoryId,
                    CategoryName = cocktail.Category.CategoryName
                },
                BartenderId = cocktail.BartenderId,
                Bartender = new BartenderDto
                {
                    BartenderId = cocktail.Bartender.BartenderId,
                    FirstName = cocktail.Bartender.FirstName,
                    LastName = cocktail.Bartender.LastName
                },
                DatePosted = cocktail.DatePosted
            };

            return cocktailDto;
        }


        public async Task<CocktailDTO> CreateCocktailAsync(CocktailDTO cocktailDto)
        {
            var bartenderExists = await _context.Bartenders.AnyAsync(b => b.BartenderId == cocktailDto.BartenderId);
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == cocktailDto.CategoryId);

            if (!bartenderExists || !categoryExists)
            {
                Console.WriteLine("❌ Error: Invalid Bartender or Category ID.");
                return null;
            }

            var cocktail = new Cocktail
            {
                DrinkName = cocktailDto.DrinkName,
                DrinkRecipe = cocktailDto.DrinkRecipe,
                LiqIns = cocktailDto.LiqIns,
                MixIns = cocktailDto.MixIns,
                BartenderId = cocktailDto.BartenderId,
                CategoryId = cocktailDto.CategoryId,
                DatePosted = cocktailDto.DatePosted
            };

            _context.Cocktails.Add(cocktail);
            await _context.SaveChangesAsync();

            cocktailDto.DrinkId = cocktail.DrinkId;
            return cocktailDto;
        }




        public async Task<bool> UpdateCocktailAsync(int id, CocktailDTO cocktailDto)
        {
            var cocktail = await _context.Cocktails.FindAsync(id);
            if (cocktail == null)
            {
                return false;
            }

            cocktail.BartenderId = cocktailDto.BartenderId;
            cocktail.DrinkName = cocktailDto.DrinkName;
            cocktail.DrinkRecipe = cocktailDto.DrinkRecipe;
            cocktail.LiqIns = cocktailDto.LiqIns;
            cocktail.MixIns = cocktailDto.MixIns;
            cocktail.CategoryId = cocktailDto.CategoryId;
            cocktail.DatePosted = cocktailDto.DatePosted;

            _context.Entry(cocktail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCocktailAsync(int id)
        {
            var cocktail = await _context.Cocktails.FindAsync(id);
            if (cocktail == null)
            {
                return false;
            }

            _context.Cocktails.Remove(cocktail);
            await _context.SaveChangesAsync();
            return true;
        }

        // Fetch all bartenders
        public async Task<IEnumerable<Bartender>> GetBartendersAsync()
        {
            return await _context.Bartenders.ToListAsync();
        }

        // Fetch all categories
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
