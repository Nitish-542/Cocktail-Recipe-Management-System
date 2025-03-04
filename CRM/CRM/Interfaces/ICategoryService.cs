using CRM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Interfaces
{
    public interface ICategoryService
    {
        // Get all categories
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        // Get a category by ID
        Task<Category> GetCategoryByIdAsync(int id);

        // Create a new category
        Task<Category> CreateCategoryAsync(Category category);

        // Update an existing category
        Task<bool> UpdateCategoryAsync(int id, Category category);

        // Delete a category by ID
        Task<bool> DeleteCategoryAsync(int id);
    }
}
