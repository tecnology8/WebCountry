using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCountry.Data;
using WebCountry.Models;
using WebCountry.ViewModel;

namespace WebCountry.Controllers
{
    public class CountryController : Controller
    {
        public readonly HumanResourceContext _context;
        public CountryController(HumanResourceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var country = await _context.Country.ToListAsync();
            return View(country);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CountryViewModel model)
        {
            var country = new Country
            {
                Name = model.Name,
                Capital = model.Capital,
                Population = model.Population,
                Economy = model.Economy,
                Currency = model.Currency
            };
            await _context.Country.AddAsync(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var country = await _context.Country.FirstOrDefaultAsync(x => x.Id == id);

            if (country != null)
            {
                var viewCountry = new UpdateCountryViewModel
                {
                    Id = country.Id,
                    Name = country.Name,
                    Capital = country.Capital,
                    Population = country.Population,
                    Economy = country.Economy,
                    Currency = country.Currency
                };

                return await Task.Run(() => View("View", viewCountry));
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> View(UpdateCountryViewModel model)
        {
            var country = await _context.Country.FindAsync(model.Id);

            if (country != null)
            {
                country.Name = model.Name;
                country.Capital = model.Capital;
                country.Population = model.Population;
                country.Economy = model.Economy;
                country.Currency = model.Currency;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCountryViewModel model)
        {
            var country = await _context.Country.FindAsync(model.Id);
            if (country != null)
            {
                _context.Country.Remove(country);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

    }
}
