using CRM.Interfaces;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    public class CategoryPageController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryPageController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: CategoryPage/List (Displays all categories)
        public async Task<IActionResult> List()
        {
            // Get all categories from the service
            var categories = await _categoryService.GetAllCategoriesAsync();

            // Map the categories to CategoryDTO
            var categoryDtos = categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList();

            return View(categoryDtos);  // Pass CategoryDTO list to the view
        }

        // GET: CategoryPage/Details/{id} (Displays details of a category)
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Map the entity to CategoryDTO
            var categoryDto = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };

            return View(categoryDto); // Pass CategoryDTO to the view
        }

        // GET: CategoryPage/Create (Displays form for creating a new category)
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryPage/Create (Handles creation of a new category)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                // Map CategoryDTO to Category model
                var category = new Category
                {
                    CategoryName = categoryDto.CategoryName
                };

                await _categoryService.CreateCategoryAsync(category);

                return RedirectToAction(nameof(List)); // Redirect to the list view after creation
            }

            return View(categoryDto); // Return the model to the view if validation fails
        }

        // GET: CategoryPage/Edit/{id} (Displays edit form for a category)
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Map the entity to CategoryDTO
            var categoryDto = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };

            return View(categoryDto); // Pass CategoryDTO to the view
        }

        // POST: CategoryPage/Edit/{id} (Handles the update of a category)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest();
            }

            var category = new Category
            {
                CategoryId = categoryDto.CategoryId,
                CategoryName = categoryDto.CategoryName
            };

            var success = await _categoryService.UpdateCategoryAsync(id, category);
            if (success)
            {
                return RedirectToAction(nameof(List)); // Redirect to the list view after update
            }

            return View(categoryDto); // Return to the edit view if update fails
        }

        // GET: CategoryPage/Delete/{id} (Displays delete confirmation for a category)
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Map the entity to CategoryDTO
            var categoryDto = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };

            return View(categoryDto); // Pass CategoryDTO to the view
        }

        // POST: CategoryPage/Delete/{id} (Handles the deletion of a category)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(List)); // Redirect to the list view after deletion
            }
            return NotFound(); // If deletion fails, return NotFound
        }
    }
}
