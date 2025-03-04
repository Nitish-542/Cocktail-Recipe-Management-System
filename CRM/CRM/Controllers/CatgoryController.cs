using CRM.Interfaces;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        // Constructor injection of ICategoryService
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // CREATE: api/category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO categoryDto)
        {
            // Map DTO to entity
            var category = new Category
            {
                CategoryName = categoryDto.CategoryName
            };

            // Call service to create the category
            var createdCategory = await _categoryService.CreateCategoryAsync(category);

            // Map entity back to DTO and return
            var createdCategoryDto = new CategoryDTO
            {
                CategoryId = createdCategory.CategoryId,
                CategoryName = createdCategory.CategoryName
            };

            return CreatedAtAction(nameof(GetCategory), new { id = createdCategoryDto.CategoryId }, createdCategoryDto);
        }

        // READ: api/category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Map entity to DTO
            var categoryDto = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };

            return categoryDto;
        }

        // READ: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            // Map list of entities to list of DTOs
            var categoryDtos = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                categoryDtos.Add(new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                });
            }

            return Ok(categoryDtos);
        }

        // UPDATE: api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest();
            }

            // Map DTO to entity
            var category = new Category
            {
                CategoryId = categoryDto.CategoryId,
                CategoryName = categoryDto.CategoryName
            };

            var success = await _categoryService.UpdateCategoryAsync(id, category);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
