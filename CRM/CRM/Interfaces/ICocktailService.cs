using CRM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Interfaces
{
    public interface ICocktailService
    {
        // Get all cocktails
        Task<IEnumerable<CocktailDTO>> GetAllCocktailsAsync();

        // Get a cocktail by ID
        Task<CocktailDTO> GetCocktailByIdAsync(int id);

        // Create a new cocktail
        Task<CocktailDTO> CreateCocktailAsync(CocktailDTO cocktailDto);

        // Update an existing cocktail
        Task<bool> UpdateCocktailAsync(int id, CocktailDTO cocktailDto);

        // Delete a cocktail by ID
        Task<bool> DeleteCocktailAsync(int id);

        // Get all bartenders (for dropdown)
        Task<IEnumerable<Bartender>> GetBartendersAsync();

        // Get all categories (for dropdown)
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
