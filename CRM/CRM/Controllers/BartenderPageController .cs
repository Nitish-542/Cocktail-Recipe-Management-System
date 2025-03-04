using CRM.Interfaces;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    public class BartenderPageController : Controller
    {
        private readonly IBartenderService _bartenderService;

        // Injecting the service into the controller
        public BartenderPageController(IBartenderService bartenderService)
        {
            _bartenderService = bartenderService;
        }

        // GET: BartenderPage/List (Display all bartenders)
        public async Task<IActionResult> List()
        {
            var bartenders = await _bartenderService.GetAllBartendersAsync();
            return View(bartenders);
        }

        // GET: BartenderPage/Details/{id} (Display bartender details)
        public async Task<IActionResult> Details(int id)
        {
            var bartender = await _bartenderService.GetBartenderByIdAsync(id);
            if (bartender == null)
            {
                return NotFound();
            }
            return View(bartender);
        }

        // GET: BartenderPage/Create (Display create form)
        public IActionResult Create()
        {
            return View();
        }

        // POST: BartenderPage/Create (Handle create form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BartenderDto bartenderDto)
        {
            if (ModelState.IsValid)
            {
                var createdBartender = await _bartenderService.CreateBartenderAsync(bartenderDto);
                return RedirectToAction(nameof(List)); // Redirect to the list view after creating
            }
            return View(bartenderDto); // If validation fails, return to the create view
        }

        // GET: BartenderPage/Edit/{id} (Display edit form)
        public async Task<IActionResult> Edit(int id)
        {
            var bartender = await _bartenderService.GetBartenderByIdAsync(id);
            if (bartender == null)
            {
                return NotFound();
            }
            return View(bartender);
        }

        // POST: BartenderPage/Edit/{id} (Handle edit form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BartenderDto bartenderDto)
        {
            if (id != bartenderDto.BartenderId)
            {
                return BadRequest();
            }

            var success = await _bartenderService.UpdateBartenderAsync(id, bartenderDto);
            if (success)
            {
                return RedirectToAction(nameof(List)); // Redirect to the list view after updating
            }
            return View(bartenderDto); // If update fails, return to the edit view
        }

        // GET: BartenderPage/Delete/{id} (Display delete confirmation)
        public async Task<IActionResult> Delete(int id)
        {
            var bartender = await _bartenderService.GetBartenderByIdAsync(id);
            if (bartender == null)
            {
                return NotFound();
            }
            return View(bartender);
        }

        // POST: BartenderPage/Delete/{id} (Handle delete form submission)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _bartenderService.DeleteBartenderAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(List)); // Redirect to the list view after deletion
            }
            return NotFound(); // If deletion fails, return NotFound
        }
    }
}
