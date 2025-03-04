using CRM.Interfaces;
using CRM.Models;
using CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    public class CocktailPageController : Controller
    {
        private readonly ICocktailService _cocktailService;
        private readonly IBartenderService _bartenderService;
        private readonly ICategoryService _categoryService;

        public CocktailPageController(ICocktailService cocktailService, IBartenderService bartenderService, ICategoryService categoryService)
        {
            _cocktailService = cocktailService;
            _bartenderService = bartenderService;
            _categoryService = categoryService;
        }

        // GET: CocktailPage/List
        public async Task<IActionResult> List()
        {
            var cocktails = await _cocktailService.GetAllCocktailsAsync();
            return View(cocktails);
        }
        // GET: CocktailPage/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var cocktail = await _cocktailService.GetCocktailByIdAsync(id);

            if (cocktail == null)
            {
                return NotFound();
            }

            var viewModel = new CocktailViewModel
            {
                DrinkId = cocktail.DrinkId,
                DrinkName = cocktail.DrinkName,
                DrinkRecipe = cocktail.DrinkRecipe,
                LiqIns = cocktail.LiqIns,
                MixIns = cocktail.MixIns,
                BartenderId = cocktail.BartenderId,
                CategoryId = cocktail.CategoryId,
                DatePosted = cocktail.DatePosted,
                Bartenders = new List<BartenderDto> { new BartenderDto
            {
                BartenderId = cocktail.BartenderId,
                FirstName = cocktail.Bartender.FirstName,
                LastName = cocktail.Bartender.LastName
            }
        },
                Categories = new List<CategoryDTO> { new CategoryDTO
            {
                CategoryId = cocktail.CategoryId,
                CategoryName = cocktail.Category.CategoryName
            }
        }
            };

            return View(viewModel);
        }



        // GET: CocktailPage/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new CocktailViewModel
            {
                Bartenders = (await _bartenderService.GetAllBartendersAsync()).ToList(),
                Categories = (await _categoryService.GetAllCategoriesAsync())
                    .Select(c => new CategoryDTO
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    })
                    .ToList()
            };

            return View(viewModel);
        }


        // POST: CocktailPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CocktailViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Bartenders = (await _bartenderService.GetAllBartendersAsync()).ToList();
                viewModel.Categories = (await _categoryService.GetAllCategoriesAsync())
                    .Select(c => new CategoryDTO
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    })
                    .ToList();

                return View(viewModel);
            }

            var cocktailDto = new CocktailDTO
            {
                DrinkName = viewModel.DrinkName,
                DrinkRecipe = viewModel.DrinkRecipe,
                LiqIns = viewModel.LiqIns,
                MixIns = viewModel.MixIns,
                BartenderId = viewModel.BartenderId,
                CategoryId = viewModel.CategoryId,
                DatePosted = viewModel.DatePosted
            };

            var createdCocktail = await _cocktailService.CreateCocktailAsync(cocktailDto);

            if (createdCocktail == null)
            {
                ModelState.AddModelError("", "Failed to create cocktail.");
                viewModel.Bartenders = (await _bartenderService.GetAllBartendersAsync()).ToList();
                viewModel.Categories = (await _categoryService.GetAllCategoriesAsync())
                    .Select(c => new CategoryDTO
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    })
                    .ToList();

                return View(viewModel);
            }

            return RedirectToAction(nameof(List));
        }

        // GET: CocktailPage/Edit/{id}
        // GET: CocktailPage/Edit/{id}
        // GET: CocktailPage/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var cocktail = await _cocktailService.GetCocktailByIdAsync(id);
            if (cocktail == null)
            {
                return NotFound();
            }

            var viewModel = new CocktailViewModel
            {
                DrinkId = cocktail.DrinkId,
                DrinkName = cocktail.DrinkName,
                DrinkRecipe = cocktail.DrinkRecipe,
                LiqIns = cocktail.LiqIns,
                MixIns = cocktail.MixIns,
                BartenderId = cocktail.BartenderId,
                CategoryId = cocktail.CategoryId,
                DatePosted = cocktail.DatePosted,
                Bartenders = (await _bartenderService.GetAllBartendersAsync())?.ToList() ?? new List<BartenderDto>(),
                Categories = (await _categoryService.GetAllCategoriesAsync())
                    ?.Select(c => new CategoryDTO
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    })
                    .ToList() ?? new List<CategoryDTO>()
            };

            return View(viewModel);
        }





        // POST: CocktailPage/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CocktailViewModel viewModel)
        {
            if (id != viewModel.DrinkId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                viewModel.Bartenders = (await _bartenderService.GetAllBartendersAsync()).ToList();
                viewModel.Categories = (await _categoryService.GetAllCategoriesAsync())
                    .Select(c => new CategoryDTO
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    })
                    .ToList();

                return View(viewModel);
            }

            var cocktailDto = new CocktailDTO
            {
                DrinkId = viewModel.DrinkId,
                DrinkName = viewModel.DrinkName,
                DrinkRecipe = viewModel.DrinkRecipe,
                LiqIns = viewModel.LiqIns,
                MixIns = viewModel.MixIns,
                BartenderId = viewModel.BartenderId,
                CategoryId = viewModel.CategoryId,
                DatePosted = viewModel.DatePosted
            };

            var success = await _cocktailService.UpdateCocktailAsync(id, cocktailDto);

            if (success)
            {
                return RedirectToAction(nameof(List)); // Redirect to the list view after update
            }

            // If update fails, repopulate dropdowns
            viewModel.Bartenders = (await _bartenderService.GetAllBartendersAsync()).ToList();
            viewModel.Categories = (await _categoryService.GetAllCategoriesAsync())
                .Select(c => new CategoryDTO
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToList();

            return View(viewModel);
        }


        // POST: CocktailPage/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _cocktailService.DeleteCocktailAsync(id);

            if (success)
            {
                return RedirectToAction(nameof(List)); // Redirect to the list view after deletion
            }

            return NotFound(); // If deletion fails
        }
    }
}
